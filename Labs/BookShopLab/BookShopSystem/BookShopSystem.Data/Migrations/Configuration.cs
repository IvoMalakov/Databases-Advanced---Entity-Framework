namespace BookShopSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookShopSystem.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopContext context)
        {
            Random random = new Random();

            SeedAuthors(context);

            var authours = context.Authors.ToList();

            SeedBooks(context, random, authours);
            SeedCategories(context, random);
        }

        private void SeedCategories(BookShopContext context, Random random)
        {
            using (var reader = new StreamReader("D:/DB Advanced/BookShopLab/BookShopSystem/resources/categories.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new char[] {' '});
                    var name = data[0];
                    var books = context.Books.ToArray();

                    HashSet<Book> booksToAdd = new HashSet<Book>()
                    {
                        books[random.Next(books.Length)],
                        books[random.Next(books.Length)],
                        books[random.Next(books.Length)]
                    };

                    context.Categories.AddOrUpdate(cat => cat.Name,
                        new Category()
                        {
                            Name = name,
                            Books = booksToAdd
                        });

                    line = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }

        private void SeedBooks(BookShopContext context, Random random, List<Author> authours)
        {
            using (var reader = new StreamReader("D:/DB Advanced/BookShopLab/BookShopSystem/resources/books.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new[] {' '}, 6);
                    var authorIndex = random.Next(0, authours.Count);
                    var author = authours[authorIndex];
                    var edition = (EditionType) int.Parse(data[0]);
                    var releaseData = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var copies = int.Parse(data[2]);
                    var price = decimal.Parse(data[3]);
                    var ageRestriction = (AgeRestriction) int.Parse(data[4]);
                    var title = data[5];

                    context.Books.AddOrUpdate(book => book.Title, 
                        new Book()
                        {
                            Author = author,
                            EditionType = edition,
                            ReleaseDate = releaseData,
                            Copies = copies,
                            Price = price,
                            AgeRestriction = ageRestriction,
                            Title = title
                        });

                    line = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }

        private void SeedAuthors(BookShopContext context)
        {
            using (var reader = new StreamReader("D:/DB Advanced/BookShopLab/BookShopSystem/resources/authors.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new char[] {' '});
                    var firstName = data[0];
                    var lastName = data[1];

                    context.Authors.AddOrUpdate(authour => authour.FirstName,
                        new Author()
                        {
                            FirstName = firstName,
                            LastName = lastName
                        });

                    line = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }
    }
}
