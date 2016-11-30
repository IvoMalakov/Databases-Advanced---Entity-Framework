namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        private ICollection<Part> parts;

        public Supplier()
        {
            this.parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }

            set { this.parts = value; }
        } 
    }
}
