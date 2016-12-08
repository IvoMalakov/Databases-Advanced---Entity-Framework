namespace MassDefect.ExportXML
{
    using System.Linq;
    using System.Xml.Linq;
    using Data;
    class Application
    {
        static void Main()
        {
            var context = new MassDefectContext();

            var exportedAnomalies = context.Anomalies
                .Select(anomaly => new
                {
                    id = anomaly.Id,
                    originPlanet = anomaly.OriginPlanet.Name,
                    teleportPlanet = anomaly.TeleportPlanet.Name,
                    victims = anomaly.Victims
                })
                .OrderBy(anomaly => anomaly.id);

            var xmlDocument = new XElement("anomalies");

            foreach (var exportedAnomalie in exportedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("teleport-planet", exportedAnomalie.teleportPlanet));
                anomalyNode.Add(new XAttribute("origin-planet", exportedAnomalie.originPlanet));
                anomalyNode.Add(new XAttribute("id", exportedAnomalie.id));

                var victimsNode = new XElement("victims");

                foreach (var victim in exportedAnomalie.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim.Name));

                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save("../../../results/anomalies.xml");
        }
    }
}
