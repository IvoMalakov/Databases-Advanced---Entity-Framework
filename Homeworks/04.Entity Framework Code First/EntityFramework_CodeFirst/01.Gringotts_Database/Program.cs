using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GringottsDB.Models;

namespace GringottsDB
{
    class Program
    {
        public static void Main()
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InstalledUICulture;
                WizardDeposit dumledore = new WizardDeposit()
                {
                    FirstName = "Albus",
                    LastName = "Dumbledore",
                    Age = 150,
                    MagicWandCreator = "Antioch Peverell",
                    MagicWandSize = 15,
                    DepositStartDate = new DateTime(2016, 10, 20),
                    DepositExpirationDate = new DateTime(2020, 10, 20),
                    DepositAmount = 2000.24m,
                    DepositCharge = 0.2,
                    IsDepositExpired = false
                };

                var context = new WizardContext();

                context.WizardDeposits.Add(dumledore);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
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
    }
}
