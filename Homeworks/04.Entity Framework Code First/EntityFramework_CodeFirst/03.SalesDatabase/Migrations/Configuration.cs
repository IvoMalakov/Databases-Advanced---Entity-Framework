namespace Sales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Sales.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<Sales.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SalesContext context)
        {
            Random rnd = new Random();
            string[] names = { "Ivan", "Gosho", "Nasko", "Maria", "Petia"};
            string[] emails = { "ala_bala@abv.bg", "boiko_borisov@abv.bg", "kircho_kirchev@gmail.com", "donald_trump@hotmail.com"};

            for (int i = 0; i < 5; i++)
            {
                context.Customers.AddOrUpdate(new Customer
                {
                    Name = names[rnd.Next(names.Length)],
                    Email = emails[rnd.Next(emails.Length)]
                });
            }

            string[] productNames = {"Kola", "DVD", "Post v pravitelstvoto", "Raketa"};

            for (int i = 0; i < 5; i++)
            {
                context.Products.AddOrUpdate(new Product
                {
                    Name = productNames[rnd.Next(productNames.Length)],
                    Price = (decimal)rnd.NextDouble() * 150m,
                    Quantity = rnd.Next(20)
                });
            }

            string[] locationNames = { "Sofia", "Varna", "Burgas", "New York", "Berlin", "Sliven" };
            for (int i = 0; i < 5; i++)
            {
                context.StoreLocations.AddOrUpdate(new StoreLocation
                {
                    LocationName = locationNames[rnd.Next(locationNames.Length)]
                });
            }


            Product[] products = context.Products.Local.ToArray();
            StoreLocation[] locations = context.StoreLocations.Local.ToArray();
            Customer[] customers = context.Customers.Local.ToArray();

            for (int i = 0; i < 5; i++)
            {
                context.Sales.AddOrUpdate(new Sale
                {
                    Product = products[rnd.Next(products.Length)],
                    Customer = customers[rnd.Next(customers.Length)],
                    StoreLocation = locations[rnd.Next(locations.Length)],
                    Date = DateTime.Now
                });
            }

            context.SaveChanges();
        }
    }
}
