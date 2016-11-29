namespace ProductsShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Product
    {
        private string name;

        private ICollection<Category> categories;

        public Product()
        {
            this.categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Name
        {
            get { return this.name; }

            set
            {
                if (!NameIsValid(value))
                {
                    throw new ArgumentException("The Name should be at least 3 symbols long");
                }

                this.name = value;
            }
        }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int SellerId { get; set; }

        [ForeignKey("SellerId")]
        public virtual User Seller { get; set; }

        public virtual User Buyer { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }

            set { this.categories = value; }
        }
    }
}
