using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("StoreLocation")]
        public int StoreLocationId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public StoreLocation StoreLocation { get; set; }
    }
}
