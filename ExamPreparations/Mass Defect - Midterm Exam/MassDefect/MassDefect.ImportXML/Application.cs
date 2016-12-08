namespace MassDefect.ImportXML
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models;
    using Data;
    class Application
    {
        private static string NewAnomaliesPath = "../../../datasets/new-anomalies.xml";

        private static string ErrorMessage = "Error: Invalid data.";
        private static string SuccessfullMessage = "Successfully imported anomaly";
        static void Main()
        {
            MassDefectContext context = new MassDefectContext();

            XDocument xml = XDocument.Load(NewAnomaliesPath);
            IEnumerable<XElement> anomalies = xml.XPathSelectElements("anomalies/anomaly");

            foreach (XElement anomalie in anomalies)
            {
                ImportAnomalyAndVictims(anomalie, context);
            }
        }

        private static void ImportAnomalyAndVictims(XElement anomalieNode, MassDefectContext context)
        {
            var teleportPlanet = anomalieNode.Attribute("teleport-planet");
            var originPlanet = anomalieNode.Attribute("origin-planet");

            if (teleportPlanet == null || originPlanet == null)
            {
                Console.WriteLine(ErrorMessage);
                return;
            }

            Anomaly anomalyEntity = new Anomaly()
            {
                OriginPlanet = GetPlanetByName(originPlanet.Value, context),
                TeleportPlanet = GetPlanetByName(teleportPlanet.Value, context)
            };

            if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
            {
                Console.WriteLine(ErrorMessage);
                return;
            }

            context.Anomalies.Add(anomalyEntity);
            Console.WriteLine(SuccessfullMessage);

            IEnumerable<XElement> victims = anomalieNode.XPathSelectElements("victims/victims");

            foreach (XElement victim in victims)
            {
                ImportVictim(victim, context, anomalyEntity);
            }

            context.SaveChanges();
        }

        private static void ImportVictim(XElement victimNode, MassDefectContext context, Anomaly anomaly)
        {
            var name = victimNode.Attribute("name");

            if (name == null)
            {
                Console.WriteLine(ErrorMessage);
                return;
            }

            Person personEntity = GetPersonByName(name.Value, context);

            if (personEntity == null)
            {
                Console.WriteLine(ErrorMessage);
                return;
            }

            anomaly.Victims.Add(personEntity);
        }

        private static Person GetPersonByName(string personName, MassDefectContext context)
        {
            Person wantedPerson = context.Persons.FirstOrDefault(person => person.Name == personName);
            return wantedPerson;
        }

        private static Planet GetPlanetByName(string planetName, MassDefectContext context)
        {
            Planet wantedPlanet = context.Planets.FirstOrDefault(planet => planet.Name == planetName);
            return wantedPlanet;
        }
    }
}
