namespace BookShopDBQuering
{
    using System;
    using System.Globalization;
    using System.Collections.Generic;
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
            //PrintBooksRelasedBeforeDate(context);
            //SearchAuthors(context);
            //SearchBooks(context);
            //SearchBookTitles(context);
            //CountBooks(context);
            //PrintTotalNumberOfBookCopiesByAuthor(context);
            PrintBookByCategory(context);
        }

        private static void PrintBookByCategory(BookShopContext context)
        {
            IDictionary<string, decimal> categoriesProfits = new Dictionary<string, decimal>();

            var categories = context.Categories;

            foreach (Category category in categories)
            {
                decimal totalProfit = 0m;

                var books = category.Books;

                foreach (Book book in books)
                {
                    decimal profitPerBook = book.Copies * book.Price;
                    totalProfit += profitPerBook;
                }

                categoriesProfits[category.Name] = totalProfit;
            }

            var orederedProfitsByCategorie = categoriesProfits
                                             .OrderByDescending(c => c.Value)
                                             .ThenBy(c => c.Key);

            foreach (var category in orederedProfitsByCategorie)
            {
                Console.WriteLine("{0} - ${1}", category.Key, category.Value); 
            }
        }

        private static void PrintTotalNumberOfBookCopiesByAuthor(BookShopContext context)
        {
            IDictionary<string, int> authorsDictionary = new Dictionary<string, int>();

            var authors = context.Authors;

            foreach (Author author in authors)
            {
                int sum = 0;

                var wantedBookCopies = author.Books
                    .Select(book => book.Copies);

                foreach (int bookCopie in wantedBookCopies)
                {
                    sum += bookCopie;
                }

                string fullName = author.FirstName + " " + author.LastName;

                authorsDictionary[fullName] = sum;
            }

            var orderedAuthorsDictionary = authorsDictionary.OrderByDescending(c => c.Value);

            foreach (var author in orderedAuthorsDictionary)
            {
                Console.WriteLine("{0} - {1}", author.Key, author.Value);
            }
        }

        private static void CountBooks(BookShopContext context)
        {
            int inputNumber = int.Parse(Console.ReadLine());

            var wantedBooks = context.Books
                .Where(book => book.Title.Length > inputNumber)
                .ToList();

            Console.WriteLine(wantedBooks.Count);
        }

        private static void SearchBookTitles(BookShopContext context)
        {
            string input = Console.ReadLine();

            var wantedBookTitles = context.Books
                .Where(book => book.Author.LastName.StartsWith(input))
                .Select(book => book.Title);

            foreach (string title in wantedBookTitles)
            {
                Console.WriteLine(title);
            }
        }

        private static void SearchBooks(BookShopContext context)
        {
            string input = Console.ReadLine().ToLower();

            var wantedBookTitles = context.Books
                .Where(book => book.Title.Contains(input))
                .Select(book => book.Title);

            foreach (string title in wantedBookTitles)
            {
                Console.WriteLine(title);
            }
        }

        private static void SearchAuthors(BookShopContext context)
        {
            string input = Console.ReadLine();

            string nativeQueryString = "SELECT (a.FirstName + ' ' + a.LastName) AS FullName " +
                                       "FROM Authors AS a " +
                                       "WHERE a.FirstName LIKE @searchString";

            SqlParameter searchString = new SqlParameter("@searchString", SqlDbType.NVarChar);
            searchString.Value = "%" + input;

            var wantedAuthors = context.Database.SqlQuery<string>(nativeQueryString, searchString);

            foreach (string author in wantedAuthors)
            {
                Console.WriteLine(author);
            }
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
