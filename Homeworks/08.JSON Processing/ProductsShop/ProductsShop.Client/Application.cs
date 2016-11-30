namespace ProductsShop.Client
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Models;
    using Data;
    class Application
    {
        static void Main()
        {
           ProductsShopContext context = new ProductsShopContext();

           //SeedDataBase(context);

           //ProductInRange(context);
           //SuccessfullySoldProducts(context);
           //CategoriesByProductCount(context);
           UsersAndProducts(context);
        }

        private static void UsersAndProducts(ProductsShopContext context)
        {
            var wantedUsers = context.Users
                .Where(user => user.ProductsSold.Count >= 1)
                .OrderByDescending(user => user.ProductsSold.Count)
                .ThenBy(user => user.LastName)
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    age = user.Age,
                    soldProducts = user.ProductsSold.Select(product => new
                    {
                        name = product.Name,
                        price = product.Price
                    })
                });

            string jsonObject = JsonConvert.SerializeObject(wantedUsers, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/users_and_products.json", jsonObject);
        }

        private static void CategoriesByProductCount(ProductsShopContext context)
        {
            var wantedCategories = context.Categories
                .OrderBy(category => category.Products.Count)
                .Select(category => new
                {
                    name = category.Name,
                    productsCoutn = category.Products.Count,
                    averagePrice = category.Products.Average(product => product.Price),
                    totalRevenue = category.Products.Sum(product => product.Price)
                });

            string jsonObject = JsonConvert.SerializeObject(wantedCategories, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/category_by_products_count.json", jsonObject);
        }

        private static void SuccessfullySoldProducts(ProductsShopContext context)
        {
            var wantedUsers = context.Users
                .Where(user => user.ProductsSold.Count(product => product.Buyer != null) > 0)
                .OrderBy(user => user.LastName)
                .ThenBy(user => user.FirstName)
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    soldProducts = user.ProductsSold
                        .Where(product => product.Buyer.FirstName != null && product.Buyer.LastName != null)
                        .Select(product => new
                        {
                            name = product.Name,
                            price = product.Price,
                            buyerFirstName = product.Buyer.FirstName,
                            buyerLastName = product.Buyer.LastName
                        })
                });

            string jsonUsers = JsonConvert.SerializeObject(wantedUsers, Formatting.Indented);
            //Console.WriteLine(jsonUsers);
            File.WriteAllText("../../../../results/successfully_sold_products.json", jsonUsers);
        }

        private static void ProductInRange(ProductsShopContext context)
        {
            var wantedProducts = context.Products
                .Where(product => product.Price > 500 && product.Price < 1000 && product.Buyer == null)
                .OrderBy(product => product.Price)
                .Select(product => new
                {
                    product.Name,
                    product.Price,
                    seller = (product.Seller.FirstName + " " + product.Seller.LastName.Trim())
                });

            string jsonObject = JsonConvert.SerializeObject(wantedProducts, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/products_in_range.json", jsonObject);
        }

        private static void SeedDataBase(ProductsShopContext context)
        {
            Random rnd = new Random();

            //SeedUsers(context);
            //SeedProducts(context, rnd);
            SeedCategories(context, rnd);

            context.SaveChanges();
        }

        private static void SeedCategories(ProductsShopContext context, Random rnd)
        {
            string jsonCategories = File.ReadAllText("../../../../resources/categories.json");
            IEnumerable<Category> importedCategories =
                JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonCategories);

            importedCategories = AddProductsToCategories(context, importedCategories, rnd);
            context.Categories.AddRange(importedCategories);
        }

        private static IEnumerable<Category> AddProductsToCategories(ProductsShopContext context ,IEnumerable<Category> importedCategories, Random rnd)
        {
            Category[] categories = importedCategories.ToArray();
            Product[] products = context.Products.ToArray();

            foreach (Category category in categories)
            {
                for (int i = 0; i < 20; i++)
                {
                    int productId = rnd.Next(0, products.Length);
                    Product product = products[productId];
                    category.Products.Add(product);
                }
            }

            return categories;
        }

        private static void SeedProducts(ProductsShopContext context, Random rnd)
        {
            string jsonProducts = File.ReadAllText("../../../../resources/products.json");
            IEnumerable<Product> importedProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonProducts);

            User[] users = context.Users.ToArray();

            importedProducts = GenerateSellers(users, importedProducts, rnd);
            importedProducts = GenerateBuyers(users, importedProducts, rnd);

            context.Products.AddRange(importedProducts);
        }

        private static IEnumerable<Product> GenerateBuyers(User[] users, IEnumerable<Product> importedProducts, Random rnd)
        {
            int numberOfUsers = users.Length;

            Product[] productsArray = importedProducts.ToArray();

            for (int i = 0; i < productsArray.Length / 2; i++)
            {
                int buyerId = rnd.Next(0, numberOfUsers);
                User buyer = users[buyerId];
                productsArray[i].Buyer = buyer;
            }

            return productsArray;
        }

        private static IEnumerable<Product> GenerateSellers(User[] users, IEnumerable<Product> importedProducts, Random rnd)
        {
            int numberOfUsers = users.Length;

            Product[] products = importedProducts.ToArray();

            foreach (Product product in products)
            {
                int sellerId = rnd.Next(0, numberOfUsers);
                User seller = users[sellerId];
                product.Seller = seller;
            }

            return products;
        }

        private static void SeedUsers(ProductsShopContext context)
        {
            string jsonUsers = File.ReadAllText("../../../../resources/users.json");
            IEnumerable<User> importedUsers = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonUsers);
            context.Users.AddRange(importedUsers);
        }
    }
}
