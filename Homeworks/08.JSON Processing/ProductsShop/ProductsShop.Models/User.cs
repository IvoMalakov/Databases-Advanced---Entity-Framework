namespace ProductsShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public partial class User
    {
        private string lastName;

        private ICollection<Product> productsBought;
        private ICollection<Product> productsSold;
        private ICollection<User> friends;

        public User()
        {
            this.productsBought = new HashSet<Product>();
            this.productsSold = new HashSet<Product>();
            this.friends = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required, MinLength(3)]
        public string LastName
        {
            get { return this.lastName; }

            set
            {
                if (!LastNameIsValid(value))
                {
                    throw new ArgumentException("Last name should be at least 3 symbols long");
                }

                this.lastName = value;
            }
        }

        public int Age { get; set; }

        [InverseProperty("Buyer")]
        public virtual ICollection<Product> ProductsBought
        {
            get { return this.productsBought; }

            set { this.productsBought = value; }
        }

        [InverseProperty("Seller")]
        public virtual ICollection<Product> ProductsSold
        {
            get { return this.productsSold; }

            set { this.productsSold = value; }
        }

        public virtual ICollection<User> Friends
        {
            get { return this.friends; }

            set { this.friends = value; }
        } 
    }
}
