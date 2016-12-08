namespace MassDefect.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Planet
    {
        private ICollection<Person> persons;
        private ICollection<Anomaly> originAnomalies;
        private ICollection<Anomaly> teleportAnomalies; 

        public Planet()
        {
            this.persons = new HashSet<Person>();
            this.originAnomalies = new HashSet<Anomaly>();
            this.teleportAnomalies = new HashSet<Anomaly>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Sun")]
        public int? SunId { get; set; }
        public virtual Star Sun { get; set; }

        [ForeignKey("SolarSystem")]
        public int? SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> Persons
        {
            get { return this.persons; }

            set { this.persons = value; }
        }

        [InverseProperty("OriginPlanet")]
        public virtual ICollection<Anomaly> OriginAnomalies
        {
            get { return this.originAnomalies; }

            set { this.originAnomalies = value; }
        }

        [InverseProperty("TeleportPlanet")]
        public virtual ICollection<Anomaly> TeleportAnomalies
        {
            get { return this.teleportAnomalies; }

            set { this.teleportAnomalies = value; }
        } 
    }
}