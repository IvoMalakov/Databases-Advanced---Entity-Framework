namespace MassDefect.ExportJSON
{
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Data;
    class Application
    {
        static void Main()
        {
            var context = new MassDefectContext();

            ExportPlanetsWichAreNotAnomalyOrigins(context);

            ExportPeopleWichHaveNotBeenVictims(context);

            ExporTopAnomaly(context);

        }

        private static void ExporTopAnomaly(MassDefectContext context)
        {
            var wantedAnomaly = context.Anomalies
                .OrderByDescending(anomaly => anomaly.Victims.Count)
                .Take(1)
                .Select(anomaly => new
                {
                    id = anomaly.Id,
                    originPlanet = new
                    {
                        name = anomaly.OriginPlanet.Name
                    },
                    victimsCount = anomaly.Victims.Count
                });

            var anomalyAsJson = JsonConvert.SerializeObject(wantedAnomaly, Formatting.Indented);
            File.WriteAllText("../../../results/anomaly.json", anomalyAsJson);
        }

        private static void ExportPeopleWichHaveNotBeenVictims(MassDefectContext context)
        {
            var wantedPersons = context.Persons
                .Where(person => !person.Anomalies.Any())
                .Select(person => new
                {
                    name = person.Name,
                    homePlanet = new
                    {
                        name = person.HomePlanet.Name
                    }
                });

            var personAsJson = JsonConvert.SerializeObject(wantedPersons, Formatting.Indented);
            File.WriteAllText("../../../results/people.json", personAsJson);
        }

        private static void ExportPlanetsWichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(planet => !planet.OriginAnomalies.Any())
                .Select(planet => new
                {
                    name = planet.Name
                });

            var planetAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText("../../../results/planets.json", planetAsJson);
        }
    }
}
