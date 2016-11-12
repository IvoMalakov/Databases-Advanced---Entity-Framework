namespace Sales.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        public Product()
        {
            this.SalesForProduct = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Sale> SalesForProduct { get; set; }
    }
}
