namespace MassDefect.ImportJSON
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using DTOs;
    using Data;
    using AutoMapper;
    using Newtonsoft.Json;
    class Application
    {
        private static string SolarSystemsPath = "../../../datasets/solar-systems.json";
        private static string PlanetsPath = "../../../datasets/planets.json";
        private static string StarsPath = "../../../datasets/stars.json";
        private static string PersonsPath = "../../../datasets/persons.json";
        private static string AnomaliesPath = "../../../datasets/anomalies.json";
        private static string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";

        private static string ErrorMessage = "Error: Invalid data.";
        private static string SuccessfullyImportMessageGlobal = "Successfully imported {0} {1}.";
        private static string SuccessfullyImportMessageAnomaly = "Successfully imported anomaly.";
        static void Main()
        {
            MassDefectContext context = new MassDefectContext();
            //context.Database.Initialize(true);
            ConfigureMapping(context);

            ImportSolarSystems(context);
            ImportStars(context);
            ImportPlanets(context);
            ImportPersons(context);
            ImportAnomalies(context);
            ImportAnomalyVictims(context);
        }

        private static void ImportAnomalyVictims(MassDefectContext context)
        {
            string json = File.ReadAllText(AnomalyVictimsPath);
            IEnumerable<AnomalyVicitimDTO> anomalyVicitimDtos =
                JsonConvert.DeserializeObject<IEnumerable<AnomalyVicitimDTO>>(json);

            foreach (AnomalyVicitimDTO anomalyVictimDto in anomalyVicitimDtos)
            {
                if (anomalyVictimDto.Id <= 0 || anomalyVictimDto.Person == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Anomaly anomaly = context.Anomalies.FirstOrDefault(an => an.Id == anomalyVictimDto.Id);
                Person victim = context.Persons.FirstOrDefault(person => person.Name == anomalyVictimDto.Person);

                if (anomaly == null || victim == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                anomaly.Victims.Add(victim);
            }

            context.SaveChanges();
        }

        private static void ImportAnomalies(MassDefectContext context)
        {
            string json = File.ReadAllText(AnomaliesPath);
            IEnumerable<AnomalyDTO> anomalyDtos = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);

            foreach (AnomalyDTO anomalyDto in anomalyDtos)
            {
                if (anomalyDto.OriginPlanet == null || anomalyDto.TeleportPlanet == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Anomaly anomalyEntity = Mapper.Map<Anomaly>(anomalyDto);

                if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Console.WriteLine(SuccessfullyImportMessageAnomaly);

                context.Anomalies.Add(anomalyEntity);
            }

            context.SaveChanges();
        }

        private static void ImportPersons(MassDefectContext context)
        {
            string json = File.ReadAllText(PersonsPath);
            IEnumerable<PersonDTO> personDtos = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);

            foreach (PersonDTO personDto in personDtos)
            {
                if (personDto.Name == null || personDto.HomePlanet == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Person personEntity = Mapper.Map<Person>(personDto);

                if (personEntity.HomePlanet == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Console.WriteLine(SuccessfullyImportMessageGlobal, nameof(Person), personEntity.Name);

                context.Persons.Add(personEntity);
            }

            context.SaveChanges();
        }

        private static void ImportPlanets(MassDefectContext context)
        {
            string json = File.ReadAllText(PlanetsPath);
            IEnumerable<PlanetDTO> planetsDtos = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

            foreach (PlanetDTO planetDto in planetsDtos)
            {
                if (planetDto.Name == null || planetDto.SolarSystem == null || planetDto.Sun == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Planet planetEntity = Mapper.Map<Planet>(planetDto);

                if (planetEntity.SolarSystem == null || planetEntity.Sun == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Console.WriteLine(SuccessfullyImportMessageGlobal, nameof(Planet), planetEntity.Name);

                context.Planets.Add(planetEntity);
            }

            context.SaveChanges();
        }

        private static void ImportStars(MassDefectContext context)
        {
            string json = File.ReadAllText(StarsPath);
            IEnumerable<StarDTO> starsDtos = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

            foreach (StarDTO starDto in starsDtos)
            {
                if (starDto.Name == null || starDto.SolarSystem == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Star starEntity = Mapper.Map<Star>(starDto);

                if (starEntity.SolarSystem == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Console.WriteLine(SuccessfullyImportMessageGlobal, nameof(Star), starEntity.Name);

                context.Stars.Add(starEntity);
            }

            context.SaveChanges();
        }

        private static void ImportSolarSystems(MassDefectContext context)
        {
            string json = File.ReadAllText(SolarSystemsPath);
            IEnumerable<SolarSystemDTO> solarSystemDtos =
                JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);

            foreach (SolarSystemDTO solarSystemDto in solarSystemDtos)
            {
                if (solarSystemDto.Name == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                SolarSystem solarSystemEntity = Mapper.Map<SolarSystem>(solarSystemDto);

                Console.WriteLine(SuccessfullyImportMessageGlobal, nameof(SolarSystem), solarSystemEntity.Name);

                context.SolarSystems.Add(solarSystemEntity);
            }

            context.SaveChanges();
        }

        private static void ConfigureMapping(MassDefectContext context)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<SolarSystemDTO, SolarSystem>();

                config.CreateMap<StarDTO, Star>()
                    .ForMember(star => star.SolarSystem,
                        configuretionExpress => configuretionExpress.MapFrom(dto => context.SolarSystems
                        .FirstOrDefault(sol => sol.Name == dto.SolarSystem)));

                config.CreateMap<PlanetDTO, Planet>()
                    .ForMember(planet => planet.SolarSystem,
                        configuretionExpress => configuretionExpress.MapFrom(dto => context.SolarSystems
                            .FirstOrDefault(sol => sol.Name == dto.SolarSystem)))
                    .ForMember(planet => planet.Sun,
                        configuretionExpress => configuretionExpress.MapFrom(dto => context.Stars
                            .FirstOrDefault(star => star.Name == dto.Sun)));

                config.CreateMap<PersonDTO, Person>()
                    .ForMember(person => person.HomePlanet,
                        configuretionExpress => configuretionExpress.MapFrom(dto => context.Planets
                            .FirstOrDefault(planet => planet.Name == dto.HomePlanet)));

                config.CreateMap<AnomalyDTO, Anomaly>()
                    .ForMember(anomaly => anomaly.OriginPlanet,
                        configuretionExpress => configuretionExpress.MapFrom(dto => context.Planets
                        .FirstOrDefault(planet => planet.Name == dto.OriginPlanet)))
                    .ForMember(anomaly => anomaly.TeleportPlanet,
                        configuretionExpress => configuretionExpress.MapFrom(dto => context.Planets
                        .FirstOrDefault(planet => planet.Name == dto.TeleportPlanet)));
            });
        }
    }
}
