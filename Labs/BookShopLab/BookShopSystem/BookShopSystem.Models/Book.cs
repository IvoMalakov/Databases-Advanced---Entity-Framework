namespace BookShopSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum EditionType
    {
        Normal, Promo, Gold
    }

    public enum AgeRestriction
    {
        Minor, Teen, Adult
    }

    public class Book
    {
        private ICollection<Category> categories;
        private ICollection<Book> realatedBooks;

        public Book()
        {
            this.categories = new HashSet<Category>();
            this.realatedBooks = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(1), MaxLength(50), Required]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public EditionType EditionType { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        public virtual Author Author { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public AgeRestriction AgeRestriction { get; set; }

        public virtual ICollection<Book> RelatedBooks
        {
            get { return this.realatedBooks; }
            set { this.realatedBooks = value; }
        }
    }
}
