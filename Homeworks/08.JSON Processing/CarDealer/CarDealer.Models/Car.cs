namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        private ICollection<Part> parts;
        private ICollection<Sale> sales;

        public Car()
        {
            this.parts = new HashSet<Part>();
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        public string Model { get; set; }

        public float? TravelledDistance { get; set; }

        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }

            set { this.parts = value; }
        }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }

            set { this.sales = value; }
        }
    }
}
