using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using UserDB.Models;

namespace UserDB
{
    class Program
    {
        static void Main()
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InstalledUICulture;

                //AddSomeUsersInDataBase();
                //AddSomeTownsToDataBase();
            }

            catch(DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult dbEntityValidationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        Console.WriteLine(dbValidationError.ErrorMessage);
                    }
                }
            }
        }

        private static void AddSomeTownsToDataBase()
        {
            var town1 = new Town
            {
                Name = "Yambol",
                CountryName = "Bulgaria"
            };

            var town2 = new Town
            {
                Name = "Sliven",
                CountryName = "Bulgaria"
            };

            var town3 = new Town
            {
                Name = "New Yourk",
                CountryName = "USA"
            };

            var town4 = new Town
            {
                Name = "Berlin",
                CountryName = "Germany"
            };

            IList<Town> towns = new List<Town>();
            towns.Add(town1);
            towns.Add(town2);
            towns.Add(town3);
            towns.Add(town4);

            var context = new UserContext();

            using (context)
            {
                context.Towns.AddRange(towns);
                context.SaveChanges();
            }
        }

        private static void AddSomeUsersInDataBase()
        {
            var ivo = new User
            {
                Username = "Ivo Ivov",
                Password = "1!Ivoo",
                Email = "ivo@abv.bg",
                RegisteredOn = new DateTime(1985, 10, 09),
                LastTimeLoggedIn = new DateTime(2010, 06, 01),
                Age = 31,
                IsDeleted = false,
            };

            var kiro = new User
            {
                Username = "Kiro Kirov",
                Password = "2@Kiro",
                Email = "kiro@abv.bg",
                RegisteredOn = new DateTime(2006, 10, 10),
                LastTimeLoggedIn = DateTime.Now,
                Age = 22,
                IsDeleted = false
            };

            var marrika = new User
            {
                Username = "Mariika",
                Password = "3#Mariika",
                Email = "mariika@abv.bg",
                Age = 15,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now,
                IsDeleted = true
            };

            IList<User> users = new List<User>();

            users.Add(ivo);
            users.Add(kiro);
            users.Add(marrika);

            var context = new UserContext();

            using (context)
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
