﻿namespace Sales.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Customer
    {

        public Customer()
        {
            this.SalesFOrCustomer = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public virtual ICollection<Sale> SalesFOrCustomer { get; set; }
    }
}
