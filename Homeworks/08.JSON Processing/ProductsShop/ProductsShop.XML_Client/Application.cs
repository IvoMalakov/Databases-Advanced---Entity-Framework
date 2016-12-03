namespace ProductsShop.XML_Client
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models;
    using Data;
    class Application
    {
        static void Main()
        {
            var context = new ProductsShopContext();
            //InitializeDatabase(context);

            //SeedDatabase(context);

            ExecuteXmlQueries(context);
        }

        private static void ExecuteXmlQueries(ProductsShopContext context)
        {
            ProdctsInRange(context);
            SuccessfullySoldProducts(context);
            CategoriesByProductCount(context);
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

            XElement xmlDocument = new XElement("users");
            xmlDocument.Add(new XAttribute("count", wantedUsers.Count()));

            foreach (var user in wantedUsers)
            {
                XElement userNode = new XElement("user");

                userNode.Add(new XAttribute("age", user.age));
                userNode.Add(new XAttribute("last-name", user.lastName));

                if (user.firstName != null)
                {
                    userNode.Add(new XAttribute("first-name", user.firstName));
                }              

                XElement soldProducts = new XElement("sold-products");
                soldProducts.Add(new XAttribute("count", user.soldProducts.Count()));

                foreach (var product in user.soldProducts)
                {
                    XElement productNode = new XElement("product");

                    productNode.Add(new XAttribute("name", product.name));
                    productNode.Add(new XAttribute("price", product.price));

                    soldProducts.Add(productNode);
                }

                userNode.Add(soldProducts);
                xmlDocument.Add(userNode);
            }

            xmlDocument.Save("../../../../results/users_and_products.xml");
        }

        private static void CategoriesByProductCount(ProductsShopContext context)
        {
            var wantedCategories = context.Categories
                .OrderBy(category => category.Products.Count)
                .Select(category => new
                {
                    name = category.Name,
                    productsCount = category.Products.Count,
                    averagePrice = category.Products.Average(product => product.Price),
                    totalRevenue = category.Products.Sum(product => product.Price)
                });

            XElement xmlDocument = new XElement("categories");

            foreach (var category in wantedCategories)
            {
                XElement categoryNode = new XElement("category");

                categoryNode.Add(new XAttribute("name", category.name));
                categoryNode.Add(new XElement("products-count", category.productsCount));
                categoryNode.Add(new XElement("average-price", category.averagePrice));
                categoryNode.Add(new XElement("total-revenue", category.totalRevenue));

                xmlDocument.Add(categoryNode);
            }

            xmlDocument.Save("../../../../results/category_by_products_count.xml");
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

            XElement xmlDocument = new XElement("users");

            foreach (var user in wantedUsers)
            {
                XElement userNode = new XElement("user");

                if (user.firstName != null)
                {
                    userNode.Add(new XAttribute("first-name", user.firstName));
                }

                userNode.Add(new XAttribute("last-name", user.lastName));

                XElement soldProducts = new XElement("sold-products");

                foreach (var product in user.soldProducts)
                {
                    XElement productNode = new XElement("product");

                    productNode.Add(new XElement("name", product.name));
                    productNode.Add(new XElement("price", product.price));

                    if (product.buyerFirstName != null)
                    {
                        productNode.Add(new XElement("buyer-first-name", product.buyerFirstName));
                    }

                    if (product.buyerLastName != null)
                    {
                        productNode.Add(new XElement("buyer-last-name", product.buyerLastName));
                    }

                    soldProducts.Add(productNode);
                }

                userNode.Add(soldProducts);

                xmlDocument.Add(userNode);
                xmlDocument.Save("../../../../results/successfully_sold_products.xml");
            }
        }

        private static void ProdctsInRange(ProductsShopContext context)
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

            XElement xmlDocument = new XElement("products");

            foreach (var product in wantedProducts)
            {
                XElement productNode = new XElement("product");
                productNode.Add(new XAttribute("name", product.Name));
                productNode.Add(new XAttribute("price", product.Price));
                productNode.Add(new XAttribute("seller", product.seller));

                xmlDocument.Add(productNode);
            }

            xmlDocument.Save("../../../../results/products_in_range.xml");
        }

        private static void InitializeDatabase(ProductsShopContext context)
        {
            context.Database.Initialize(true);
        }

        private static void SeedDatabase(ProductsShopContext context)
        {
            Random rnd = new Random();

            //SeedUsers(context);
            //SeedCategories(context);
            SeedProducts(context, rnd);
        }

        private static void SeedCategories(ProductsShopContext context)
        {
            XDocument xml = XDocument.Load("../../../../resources/categories.xml");
            var categories = xml.XPathSelectElements("categories/category");

            foreach (var categoryNote in categories)
            {
                SeedOneCategoryToDataBase(context, categoryNote);
            }

            context.SaveChanges();
        }

        private static void SeedOneCategoryToDataBase(ProductsShopContext context, XElement categoryNote)
        {
            string name = categoryNote.Element("name").Value;

            Category category = new Category()
            {
                Name = name
            };

            context.Categories.Add(category);
        }

        private static void SeedProducts(ProductsShopContext context, Random rnd)
        {
            XDocument xml = XDocument.Load("../../../../resources/products.xml");
            var products = xml.XPathSelectElements("products/product");

            foreach (XElement product in products)
            {
                SeedOneProductToDataBase(product, rnd, context);
            }

            context.SaveChanges();
        }

        private static void SeedOneProductToDataBase(XElement productNote, Random rnd, ProductsShopContext context)
        {
            string name = productNote.Element("name").Value;
            decimal price = (decimal) productNote.Element("price");

            Product product = new Product()
            {
                Name = name,
                Price = price
            };

            User[] users = context.Users.ToArray();
            product = GenerateSeller(product, users, rnd);
            product = GenerateBuyer(product, users, rnd);

            Category[] categories = context.Categories.ToArray();
            product = AddSomeCategoriesToProduct(product, categories, rnd);

            context.Products.Add(product);
        }

        private static Product AddSomeCategoriesToProduct(Product product, Category[] categories, Random rnd)
        {
            for (int i = 0; i < 7; i++)
            {
                int categoryId = rnd.Next(0, categories.Length);
                Category category = categories[categoryId];
                product.Categories.Add(category);
            }

            return product;
        }

        private static Product GenerateBuyer(Product product, User[] users, Random rnd)
        {
            int buyerId = rnd.Next(0, users.Length);
            User buyer = null;

            if (buyerId % 7 != 0)
            {
                buyer = users[buyerId];
            }

            product.Buyer = buyer;
            return product;
        }

        private static Product GenerateSeller(Product product, User[] users, Random rnd)
        {
            int sellerId = rnd.Next(0, users.Length);
            User seller = users[sellerId];
            product.Seller = seller;

            return product;
        }

        private static void SeedUsers(ProductsShopContext context)
        {
            XDocument xml = XDocument.Load("../../../../resources/users.xml");
            var users = xml.XPathSelectElements("users/user");

            foreach (var user in users)
            {
                SeedOneUserToDataBase(user, context);
            }

            context.SaveChanges();
        }

        private static void SeedOneUserToDataBase(XElement userNote, ProductsShopContext context)
        {
            var firstName = userNote.Attribute("first-name");
            var lastName = userNote.Attribute("last-name");
            int age = 0;

            if (userNote.Attribute("age") != null)
            {
                age = (int) userNote.Attribute("age");
            }

            var user = new User();

            if (firstName == null)
            {
                user.LastName = lastName.Value;
                user.Age = age;
            }

            else
            {
                user.FirstName = firstName.Value;
                user.LastName = lastName.Value;
                user.Age = age;
            }

            context.Users.Add(user);
        }
    }
}
