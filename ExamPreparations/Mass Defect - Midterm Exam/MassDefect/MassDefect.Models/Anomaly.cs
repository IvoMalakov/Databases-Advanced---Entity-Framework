namespace MassDefect.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Anomaly
    {
        private ICollection<Person> victims;

        public Anomaly()
        {
            this.victims = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("OriginPlanet")]
        public int? OriginPlanetId { get; set; }

        public virtual Planet OriginPlanet { get; set; }

        [ForeignKey("TeleportPlanet")]
        public int? TeleportPlanetId { get; set; }

        public virtual Planet TeleportPlanet { get; set; }

        public virtual ICollection<Person> Victims
        {
            get { return this.victims; }

            set { this.victims = value; }
        } 
    }
}
