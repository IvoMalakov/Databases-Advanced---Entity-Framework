namespace ProductsShop.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Product> Products { get; set; } 

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.Friends)
                .WithMany()
                .Map(config =>
                {
                    config.MapLeftKey("UserId");
                    config.MapRightKey("FriendId");
                    config.ToTable("UserFriends");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}