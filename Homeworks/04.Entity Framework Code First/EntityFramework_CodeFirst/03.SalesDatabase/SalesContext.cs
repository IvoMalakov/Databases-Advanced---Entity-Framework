namespace Sales
{
    using System.Data.Entity;
    using Sales.Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
            : base("name=SalesContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<SalesContext>());
            this.Database.Initialize(true);
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        public IDbSet<StoreLocation> StoreLocations { get; set; }

        public IDbSet<Sale> Sales { get; set; }
    }
}