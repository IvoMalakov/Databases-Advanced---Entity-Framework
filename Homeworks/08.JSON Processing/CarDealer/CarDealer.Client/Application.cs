namespace CarDealer.Client
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
            CarDealerContext context = new CarDealerContext();
            //InitializeBase(context);

            //SeedDatabase(context);

            //OrderedCustomers(context);
            //CarsFromMakeToyota(context);
            //LocalSuppliers(context);
            //CarsWithTheirListOfParts(context);
            //TotalSalesByCustomer(context);
            SalesWithAppliedDiscount(context);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var wantedSales = context.Sales
                .Select(sale => new
                {
                    car = new
                    {
                        sale.Car.Make,
                        sale.Car.Model,
                        sale.Car.TravelledDistance
                    },
                    customerName = sale.Customer.Name,
                    sale.Discount,
                    price = sale.Car.Parts.Sum(part => part.Price),
                    priceWithDiscount = sale.Car.Parts.Sum(part => part.Price)*(1 - sale.Discount)
                });

            string jsonObject = JsonConvert.SerializeObject(wantedSales, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/sales_with_applied_discounts.json", jsonObject);
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var wantedCustomers = context.Customers
                .Where(customer => customer.Sales.Count >= 1)
                .Select(customer => new
                {
                    fullName = customer.Name,
                    boughtCars = customer.Sales.Count(sale => sale.Customer.Id == customer.Id),
                    spentMoney = customer.Sales.Sum(sale => sale.Car.Parts.Sum(part => part.Price))
                })
                .OrderByDescending(customer => customer.spentMoney)
                .ThenByDescending(customer => customer.boughtCars);

            string jsonObject = JsonConvert.SerializeObject(wantedCustomers, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/total_sales_by_customer.json", jsonObject);
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var wantedCars = context.Cars
                .Select(car => new
                {
                    car = new
                    {
                        car.Make,
                        car.Model,
                        car.TravelledDistance,
                        parts = car.Parts.Select(part => new
                        {
                            part.Name,
                            part.Price
                        })
                    }
                });

            string jsonObject = JsonConvert.SerializeObject(wantedCars, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/cars_with_their_list_of_parts.json", jsonObject);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var wantedSuppliers = context.Suppliers
                .Where(sup => sup.IsImporter == false)
                .Select(sup => new
                {
                    sup.Id,
                    sup.Name,
                    partsCount = sup.Parts.Count
                });

            string jsonObject = JsonConvert.SerializeObject(wantedSuppliers, Formatting.Indented);
            //Console.WriteLine(jsonObject);
            File.WriteAllText("../../../../results/local_suppliers.json", jsonObject);
        }

        private static void CarsFromMakeToyota(CarDealerContext context)
        {
            var wantedCars = context.Cars
                .Where(car => car.Make == "Toyota")
                .OrderBy(car => car.Model)
                .ThenByDescending(car => car.TravelledDistance)
                .Select(car => new
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                });

            string jsonString = JsonConvert.SerializeObject(wantedCars, Formatting.Indented);
            //Console.WriteLine(jsonString);
            File.WriteAllText("../../../../results/cars_from_make_toyota.json", jsonString);
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            var wantedCustomers = context.Customers
                .OrderBy(customer => customer.BirthDate)
                .ThenBy(customer => customer.IsYoungDriver)
                .Select(customer => new
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    BirthDate = customer.BirthDate,
                    IsYoungDriver = customer.IsYoungDriver,
                    Sales = "[]"
                });

            string jsonString = JsonConvert.SerializeObject(wantedCustomers, Formatting.Indented);
            //Console.WriteLine(jsonString);
            File.WriteAllText("../../../../results/ordered_customers.json", jsonString);
        }

        private static void InitializeBase(CarDealerContext context)
        {
            context.Database.Initialize(true);
        }

        private static void SeedDatabase(CarDealerContext context)
        {
            Random rnd = new Random();

            SeedSuppliers(context);
            SeedParts(context, rnd);
            SeedCars(context, rnd);
            SeedCustomers(context);
            SeedSales(context, rnd);
        }

        private static void SeedSales(CarDealerContext context, Random rnd)
        {
            Car[] cars = context.Cars.ToArray();
            Customer[] customers = context.Customers.ToArray();

            decimal[] discountArray = new decimal[]
            {
                0m,
                0.05m,
                0.1m,
                0.15m,
                0.2m,
                0.3m,
                0.4m,
                0.5m
            };

            for (int i = 0; i < 150; i++)
            {
                int carId = rnd.Next(0, cars.Length);
                Car car = cars[carId];

                int customerId = rnd.Next(0, customers.Length);
                Customer customer = customers[customerId];

                int discountIndex = rnd.Next(0, discountArray.Length);
                decimal discountRate = discountArray[discountIndex];

                if (customer.IsYoungDriver)
                {
                    discountRate += 0.05m;
                }

                Sale sale = new Sale()
                {
                    Car = car,
                    Customer = customer,
                    Discount = discountRate
                };

                context.Sales.Add(sale);
            }

            context.SaveChanges();
        }

        private static void SeedCustomers(CarDealerContext context)
        {
            string jsonObject = File.ReadAllText("../../../../resources/customers.json");
            ICollection<Customer> customers = JsonConvert.DeserializeObject<ICollection<Customer>>(jsonObject);

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void SeedCars(CarDealerContext context, Random rnd)
        {
            string jsonObject = File.ReadAllText("../../../../resources/cars.json");
            ICollection<Car> cars = JsonConvert.DeserializeObject<ICollection<Car>>(jsonObject);
            Part[] parts = context.Parts.ToArray();

            foreach (Car car in cars)
            {
                int numberOfPartsToAdd = rnd.Next(10, 21);

                for (int i = 0; i < numberOfPartsToAdd; i++)
                {
                    int partId = rnd.Next(0, parts.Length);
                    Part part = parts[partId];
                    car.Parts.Add(part);
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void SeedParts(CarDealerContext context, Random rnd)
        {
            string jsonObject = File.ReadAllText("../../../../resources/parts.json");
            ICollection<Part> parts = JsonConvert.DeserializeObject<ICollection<Part>>(jsonObject);
            Supplier[] suppliers = context.Suppliers.ToArray();

            foreach (Part part in parts)
            {
                int supplierId = rnd.Next(0, suppliers.Length);
                Supplier supplier = suppliers[supplierId];
                part.Supplier = supplier;
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void SeedSuppliers(CarDealerContext context)
        {
            string jsonObject = File.ReadAllText("../../../../resources/suppliers.json");
            ICollection<Supplier> suppliers = JsonConvert.DeserializeObject<ICollection<Supplier>>(jsonObject);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}