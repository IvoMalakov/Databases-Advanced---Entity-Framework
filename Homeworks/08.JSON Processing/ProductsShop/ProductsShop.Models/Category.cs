namespace ProductsShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Category
    {
        private string name;

        private ICollection<Product> products;

        public Category()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(15)]
        public string Name
        {
            get { return this.name; }

            set
            {
                if (!NameIsValid(value))
                {
                    throw new ArgumentException("The Name should be between 3 ad 15 symbols long");
                }

                this.name = value;
            }
        }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }

            set { this.products = value; }
        } 
    }
}
