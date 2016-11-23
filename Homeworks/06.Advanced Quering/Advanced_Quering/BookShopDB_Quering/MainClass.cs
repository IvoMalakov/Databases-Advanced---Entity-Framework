namespace BookShopDBQuering
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Data;
    using System.Data.SqlClient;
    using BookShopSystem.Data;
    using BookShopSystem.Models;
    public class MainClass
    {
        static void Main()
        {
            var context = new BookShopContext();

            //PrintBookTitlesByAgeRestriction(context);
            //PrintGoledenBooks(context);
            //PrintBooksByPrice(context);
            //PrintNotReleasedBooks(context);
            //PrintBookTitlesByCategory(context);
            PrintBooksRelasedBeforeDate(context);
        }

        private static void PrintBooksRelasedBeforeDate(BookShopContext context)
        {
            string input = Console.ReadLine();

            DateTime givenDate = DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var wantedBooks = context.Books
                .Where(book => book.ReleaseDate.Value < givenDate)
                .Select(book => new
                {
                    book.Title,
                    book.EditionType,
                    book.Price
                });

            foreach (var book in wantedBooks)
            {
                Console.WriteLine("{0} - {1} - {2}", book.Title, book.EditionType, book.Price);
            }
        }

        private static void PrintBookTitlesByCategory(BookShopContext context)
        {
            string input = Console.ReadLine();

            string[] categories = input
                .ToLower()
                .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string category in categories)
            {             
                string nativeQueryString = "SELECT b.Title " +
                                           "FROM Books As b " +
                                           "INNER JOIN CategoryBooks AS cb " +
                                           "ON b.Id = cb.Book_Id " +
                                           "INNER JOIN Categories AS c " +
                                           "ON c.Id = cB.Category_Id " +
                                           "WHERE c.Name = @category";

                SqlParameter categoryName = new SqlParameter("@category", SqlDbType.NVarChar);
                categoryName.Value = category;

                var wantedBookTitles = context.Database.SqlQuery<string>(nativeQueryString, categoryName);

                foreach (string title in wantedBookTitles)
                {
                    Console.WriteLine(title);
                }
            }
        }

        private static void PrintNotReleasedBooks(BookShopContext context)
        {
            int givenYear = int.Parse(Console.ReadLine());

            var wantedBookTitles = context.Books
                .Where(book => book.ReleaseDate.Value.Year != givenYear)
                .Select(book => book.Title);

            foreach (string title in wantedBookTitles)
            {
                Console.WriteLine(title);
            }
        }

        private static void PrintBooksByPrice(BookShopContext context)
        {
            var wantedBooks = context.Books
                .Where(book => book.Price < 5 || book.Price > 40)
                .Select(book => new
                {
                    book.Title,
                    book.Price
                });

            foreach (var book in wantedBooks)
            {
                Console.WriteLine("{0} - ${1}", book.Title, book.Price);
            }
        }

        private static void PrintGoledenBooks(BookShopContext context)
        {
            var wantedBooks = context.Books
                .Where(book => book.EditionType == EditionType.Gold && book.Copies < 5000)
                .Select(book => book.Title);

            foreach (string title in wantedBooks)
            {
                Console.WriteLine(title);
            }
        }

        private static void PrintBookTitlesByAgeRestriction(BookShopContext context)
        {
            string input = Console.ReadLine();

            int restriction;

            if (input != null && input.ToLower() == "minor")
            {
                restriction = 0;
            }

            else if (input != null && input.ToLower() == "teen")
            {
                restriction = 1;
            }

            else if (input != null && input.ToLower() == "adult")
            {
                restriction = 2;
            }

            else
            {
                throw new ArgumentException("Invalid input");
            }

            var wantedBooksTitles = context.Books
                .Where(book => book.AgeRestriction == (AgeRestriction) restriction)
                .Select(book => book.Title);

            foreach (string title in wantedBooksTitles)
            {
                Console.WriteLine(title);
            }
        }
    }
}
