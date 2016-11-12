namespace Sales.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.Collections.Generic;
    public class StoreLocation
    {
        public StoreLocation()
        {
            this.SalesInStore = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(50), Required]
        public string LocationName { get; set; }

        public virtual ICollection <Sale> SalesInStore { get; set; }
    }
}
