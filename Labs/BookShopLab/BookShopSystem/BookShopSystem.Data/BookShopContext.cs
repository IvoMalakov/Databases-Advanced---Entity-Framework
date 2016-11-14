namespace BookShopSystem.Data
{
    using System;
    using System.Data.Entity;
    using BookShopSystem.Models;

    public class BookShopContext : DbContext
    {

        public BookShopContext()
            : base("name=BookShopContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BookShopContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(book => book.RelatedBooks)
                .WithMany()
                .Map(configuration =>
                {
                    configuration.MapLeftKey("BookId");
                    configuration.MapRightKey("RelatedBookId");
                    configuration.ToTable("RelatedBooks");
                });

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Category> Categories { get; set; }
    }
}