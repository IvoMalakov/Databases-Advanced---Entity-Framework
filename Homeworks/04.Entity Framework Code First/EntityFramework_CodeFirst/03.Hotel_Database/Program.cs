using System;
using Hotel.Models;
using System.Globalization;
using System.Data.Entity.Validation;

namespace Hotel
{
    class Program
    {
        static void Main()
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InstalledUICulture;

                var context = new HotelContext();

                using (context)
                {
                    var employee = new Employee
                    {
                        FirstName = "Ivo",
                        LastName = "Malakov",
                        Title = "Boss",
                        Notes = "Nai-dobria be"
                    };

                    var customer = new Customer
                    {
                        FirstName = "Penka",
                        LastName = "Penkova",
                        PhoneNumber = "+359886000000"
                    };

                    var roomStatus = new RoomStatus
                    {
                        Status = RoomStatuses.Free
                    };

                    var roomtype = new RoomType
                    {
                        Type = RoomTypes.Large
                    };

                    var bedtype = new BedType
                    {
                        Type = BedTypes.KingSize
                    };

                    var room = new Room
                    {
                        RoomType = roomtype,
                        BedType = bedtype,
                        Rate = 10m,
                        RoomStatus = roomStatus,
                        Notes = "Mnogo hubava staqa"
                    };

                    var payment = new Payment
                    {
                        PaymentDate = new DateTime(2016, 11, 09),
                        AccountNumber = 1000,
                        FirstDateOccupied = new DateTime(2016, 11, 05),
                        LastDateOccupied = new DateTime(2016, 11, 08),
                        TotalDays = 4,
                        AmountCharged = 1000m,
                        TaxRate = 1m,
                        TaxAmount = 2m,
                        PaymentTotal = 1003m
                    };

                    var occupancie = new Occupancie
                    {
                        DateOccupied = new DateTime(2016, 11, 05),
                        AccountNumber = 1000,
                        RoomNumber = 303,
                        RateApplied = 100m,
                        PhoneCharge = 130m,
                        Notes = "I am the best"
                    };

                    context.Employees.Add(employee);
                    context.Customers.Add(customer);
                    context.RoomStatus.Add(roomStatus);
                    context.RoomTypes.Add(roomtype);
                    context.Rooms.Add(room);
                    context.Payments.Add(payment);
                    context.Occupancies.Add(occupancie);
                    context.SaveChanges();
                }
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
