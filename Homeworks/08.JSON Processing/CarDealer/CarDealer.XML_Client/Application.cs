namespace CarDealer.XML_Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models;
    using Data;

    class Application
    {
        static void Main()
        {
            CarDealerContext context = new CarDealerContext();
            //InitializeDatabase(context);

            //SeedDatabse(context);

            ExecuteQueries(context);
        }

        private static void SeedDatabse(CarDealerContext context)
        {
            Random rnd = new Random();

            //SeedSuppliers(context);
            //SeedParts(context, rnd);
            //SeedCars(context, rnd);
            //SeedCustomers(context);
            //SeedSales(context, rnd);
        }

        private static void ExecuteQueries(CarDealerContext context)
        {
            OrderedCustomers(context);
            CarsFromMakeToyota(context);
            LocalSuppliers(context);
            CarsWithTheirListOfParts(context);
            TotalSalesByCustomer(context);
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
                    priceWithDiscount = sale.Car.Parts.Sum(part => part.Price) * (1 - sale.Discount)
                });

            XElement xml = new XElement("sales");

            foreach (var sale in wantedSales)
            {
                XElement saleNode = new XElement("sale");

                XElement carNode = new XElement("car");
                carNode.Add(new XAttribute("travelled-distance", sale.car.TravelledDistance));
                carNode.Add(new XAttribute("model", sale.car.Model));
                carNode.Add(new XAttribute("make", sale.car.Make));
                saleNode.Add(carNode);

                saleNode.Add(new XElement("customer-name", sale.customerName));
                saleNode.Add(new XElement("discount", sale.Discount));
                saleNode.Add(new XElement("price", sale.price));
                saleNode.Add(new XElement("price-with-discount", sale.priceWithDiscount));

                xml.Add(saleNode);
            }

            xml.Save("../../../../results/sales_with_applied_discounts.xml");
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

            XElement xml = new XElement("customers");

            foreach (var customer in wantedCustomers)
            {
                XElement customerNode = new XElement("customer");

                customerNode.Add(new XAttribute("spent-money", customer.spentMoney));
                customerNode.Add(new XAttribute("bought-cars", customer.boughtCars));
                customerNode.Add(new XAttribute("full-name", customer.fullName));

                xml.Add(customerNode);
            }

            xml.Save("../../../../results/total_sales_by_customer.xml");
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var wantedCars = context.Cars
                .Select(car => new
                {
                    car.Make,
                    car.Model,
                    car.TravelledDistance,
                    parts = car.Parts.Select(part => new
                    {
                        part.Name,
                        part.Price
                    })
                });

            XElement xml = new XElement("cars");

            foreach (var car in wantedCars)
            {
                XElement carNode = new XElement("car");

                carNode.Add(new XAttribute("travelled-distance", car.TravelledDistance));
                carNode.Add(new XAttribute("model", car.Model));
                carNode.Add(new XAttribute("make", car.Make));

                XElement partsElement = new XElement("parts");

                foreach (var part in car.parts)
                {
                    XElement partNode = new XElement("part");

                    partNode.Add(new XAttribute("price", part.Price));
                    partNode.Add(new XAttribute("name", part.Name));

                    partsElement.Add(partNode);
                }

                carNode.Add(partsElement);

                xml.Add(carNode);
                xml.Save("../../../../results/cars_with_their_list_of_parts.xml");
            }
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

            XElement xml = new XElement("suppliers");

            foreach (var supplier in wantedSuppliers)
            {
                XElement supplierNode = new XElement("supplier");

                supplierNode.Add(new XAttribute("parts-count", supplier.partsCount));
                supplierNode.Add(new XAttribute("name", supplier.Name));
                supplierNode.Add(new XAttribute("id", supplier.Id));

                xml.Add(supplierNode);
            }

            xml.Save("../../../../results/local_suppliers.xml");
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

            XElement xml = new XElement("cars");

            foreach (var car in wantedCars)
            {
                XElement carNode = new XElement("car");

                carNode.Add(new XAttribute("travelled-distance", car.TravelledDistance));
                carNode.Add(new XAttribute("model", car.Model));
                carNode.Add(new XAttribute("make", car.Make));
                carNode.Add(new XAttribute("id", car.Id));

                xml.Add(carNode);
            }

            xml.Save("../../../../results/cars_from_make_toyota.xml");
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
                    IsYoungDriver = customer.IsYoungDriver
                });

            XElement xmlDocument = new XElement("customers");

            foreach (var customer in wantedCustomers)
            {
                XElement customerNode = new XElement("customer");

                customerNode.Add(new XElement("id", customer.Id));
                customerNode.Add(new XElement("name", customer.Name));
                customerNode.Add(new XElement("birth-date", customer.BirthDate));
                customerNode.Add(new XElement("is-young-driver", customer.IsYoungDriver));

                xmlDocument.Add(customerNode);
            }

            xmlDocument.Save("../../../../results/ordered_customers.xml");
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
            XDocument xmlDocument = XDocument.Load("../../../../resources/customers.xml");
            var customers = xmlDocument.XPathSelectElements("customers/customer");

            foreach (var customer in customers)
            {
                SeedOneCustomerToDatabase(customer, context);
            }

            context.SaveChanges();
        }

        private static void SeedOneCustomerToDatabase(XElement customerNode, CarDealerContext context)
        {
            string name = customerNode.Attribute("name").Value;
            DateTime birthDate = (DateTime) customerNode.Element("birth-date");
            bool isYoungDriver = (bool) customerNode.Element("is-young-driver");

            Customer customer = new Customer()
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            context.Customers.Add(customer);
        }

        private static void SeedCars(CarDealerContext context, Random rnd)
        {
            XDocument xmlDocument = XDocument.Load("../../../../resources/cars.xml");
            var cars = xmlDocument.XPathSelectElements("cars/car");
            Part[] parts = context.Parts.ToArray();

            foreach (var car in cars)
            {
                SeedOneCarToDatabase(car, parts, rnd, context);
            }

            context.SaveChanges();
        }

        private static void SeedOneCarToDatabase(XElement carNode, Part[] parts, Random rnd, CarDealerContext context)
        {
            string make = carNode.Element("make").Value;
            string model = carNode.Element("model").Value;
            float travelledDistance = (float) carNode.Element("travelled-distance");

            IList<Part> partsList = new List<Part>();
            int numberOfPartsForAdd = rnd.Next(10, 21);

            for (int i = 0; i < numberOfPartsForAdd; i++)
            {
                int partId = rnd.Next(0, parts.Length);
                Part part = parts[partId];
                partsList.Add(part);
            }

            Car car = new Car()
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance,
                Parts = partsList
            };

            context.Cars.Add(car);
        }

        private static void SeedParts(CarDealerContext context, Random rnd)
        {
            XDocument xmlDocument = XDocument.Load("../../../../resources/parts.xml");
            var parts = xmlDocument.XPathSelectElements("parts/part");
            Supplier[] suppliers = context.Suppliers.ToArray();

            foreach (var part in parts)
            {
                SeedOnePartToDataBase(part, suppliers, rnd, context);
            }

            context.SaveChanges();
        }

        private static void SeedOnePartToDataBase(XElement partNode, Supplier[] suppliers, Random rnd, CarDealerContext context)
        {
            string name = partNode.Attribute("name").Value;
            decimal price = (decimal)partNode.Attribute("price");
            int quantity = (int)partNode.Attribute("quantity");

            int supplierId = rnd.Next(0, suppliers.Length);
            Supplier supplier = suppliers[supplierId];

            Part part = new Part()
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Supplier = supplier
            };

            context.Parts.Add(part);
        }

        private static void SeedSuppliers(CarDealerContext context)
        {
            XDocument xmlDocument = XDocument.Load("../../../../resources/suppliers.xml");
            var suppliers = xmlDocument.XPathSelectElements("suppliers/supplier");

            foreach (var supplier in suppliers)
            {
                SeedOneSupplierToDatabase(supplier, context);
            }

            context.SaveChanges();
        }

        private static void SeedOneSupplierToDatabase(XElement supplierNode, CarDealerContext context)
        {
            string name = supplierNode.Attribute("name").Value;
            bool isImporter = (bool) supplierNode.Attribute("is-importer");

            Supplier supplier = new Supplier()
            {
                Name = name,
                IsImporter = isImporter
            };

            context.Suppliers.Add(supplier);
        }

        private static void InitializeDatabase(CarDealerContext context)
        {
            context.Database.Initialize(true);
        }
    }
}
