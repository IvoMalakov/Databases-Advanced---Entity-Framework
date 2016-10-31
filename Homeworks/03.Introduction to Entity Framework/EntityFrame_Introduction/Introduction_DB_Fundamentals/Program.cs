using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using Introduction_DB_Fundamentals.Models;

namespace Introduction_DB_Fundamentals
{
    internal class Program
    {
        private static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InstalledUICulture;

            //Task3_EmployeesFullInformation();
            //Task4_EmployeesWithSalaryOver50000();
            //Task5_EmployeesFromResearchAndDevelopment();
            //Task6_AddingNewAdressAndUpdatingEmployee();
            //Task7_DeleteProjectById();
            //Task8_FindEmployeesInPeriod();
            //Task9_AdressesByTownName();
            //Task10_EmployeeWithId147SortedByProjectNames();
            //Task11_DepartmentsWithMoreThan5Employees();
            //Task15_FindLatest10Projects();
            //Task16_IncreaseSalaries();
            //Task17_Remove_Towns();
            //Task18_FindEmployeeByFirstNameStartingWithSA();
            Task19_FirstLetter();
        }

        private static void Task19_FirstLetter()
        {
            IList<char> wizzardFirstNameLetterList = new List<char>();
            var context = new GringottsContext();

            using (context)
            {
                var wizzardNames = context.WizzardDeposits
                    .Where(c => c.DepositGroup == "Troll Chest")
                    .Select(d => d.FirstName);

                foreach (var wizzardName in wizzardNames)
                {
                    char letter = Convert.ToChar(wizzardName.Substring(0, 1));
                    wizzardFirstNameLetterList.Add(letter);
                }

                var wizardsUniqueLetters = wizzardFirstNameLetterList.Distinct().OrderBy(c => c);

                foreach (var letter in wizardsUniqueLetters)
                {
                    Console.WriteLine(letter);
                }
            }
        }

        private static void Task18_FindEmployeeByFirstNameStartingWithSA()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var employees = context.Employees
                    .Where(c => c.FirstName.StartsWith("SA"));

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary})");
                }
            }
        }

        private static void Task17_Remove_Towns()
        {
            string townName = Console.ReadLine();

            var context = new SoftuniContext();

            using (context)
            {
                var town = context.Towns
                    .FirstOrDefault(t => t.Name == townName);

                var adresses = context.Addresses
                                      .Where(c => c.Town.Name == townName);

                    foreach (var adress in adresses)
                    {
                        adress.Employees.Clear();
                    }

                if (town != null)
                {
                    town.Addresses.Clear();
                    context.Towns.Remove(town);
                }                  
                
                Console.WriteLine("{0} adresses in {1} was deleted", adresses.Count(), townName);
            }
        }

        private static void Task16_IncreaseSalaries()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var emnployees = context.Employees
                    .Where(employee => employee.Department.Name == "Engineering"
                                       || employee.Department.Name == "Tool Design"
                                       || employee.Department.Name == "Marketing"
                                       || employee.Department.Name == "Information Services");

                foreach (var employee in emnployees)
                {
                    employee.Salary += employee.Salary*(decimal) 0.12;

                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F6})");
                }

                context.SaveChanges();
            }
        }

        private static void Task15_FindLatest10Projects()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var projects = context.Projects
                    .OrderByDescending(project => project.StartDate)
                    .Take(10)
                    .OrderBy(project => project.Name);

                StringBuilder output = new StringBuilder();

                foreach (var project in projects)
                {
                    output.AppendLine($"{project.Name} {project.Description} {project.StartDate} {project.EndDate}");
                }

                var file = new System.IO.StreamWriter(@"../../latest10projects.txt");

                using (file)
                {
                    file.Write(output);
                }
            }
        }

        private static void Task11_DepartmentsWithMoreThan5Employees()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var departments = context.Departments
                    .Where(c => c.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count);

                foreach (var department in departments)
                {
                    Console.WriteLine($"{department.Name} {department.Manager.FirstName}");

                    foreach (var employee in department.Employees)
                    {
                        Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                    }
                }
            }
        }

        private static void Task10_EmployeeWithId147SortedByProjectNames()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var employee = context.Employees
                    .FirstOrDefault(c => c.EmployeeID == 147);

                if (employee != null)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");

                    var projects = employee.Projects.OrderBy(project => project.Name);

                    foreach (var project in projects)
                    {
                        Console.WriteLine($"{project.Name}");
                    }
                }
            }
        }

        private static void Task9_AdressesByTownName()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var adresses = context.Addresses
                    .OrderByDescending(c => c.Employees.Count)
                    .ThenBy(t => t.Town.Name)
                    .Take(10);

                foreach (var adress in adresses)
                {
                    Console.WriteLine($"{adress.AddressText}, {adress.Town.Name} - {adress.Employees.Count} employees");
                }
            }
        }

        private static void Task8_FindEmployeesInPeriod()
        {
           var context = new SoftuniContext();

           using (context)
           {
               var employees = context.Employees
                   .Where(
                       employee =>
                           employee.Projects.Count(
                               project => project.StartDate.Year >= 2001 && project.StartDate.Year <= 2003) > 0)
                   .Take(30);

               foreach (var employee in employees)
               {
                   Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");

                   foreach (var project in employee.Projects)
                   {
                       Console.WriteLine($"--{project.Name} {project.StartDate} {project.EndDate}");
                   }
               }
           }
        }

        private static void Task7_DeleteProjectById()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var project = context.Projects.Find(2);

                project.Employees.Clear();
                context.Projects.Remove(project);
                context.SaveChanges();

                var allProjectsLeft = context.Projects
                    .Select(a => a.Name)
                    .Take(10);

                foreach (var projectName in allProjectsLeft)
                {
                    Console.WriteLine(projectName);
                }
            }
        }

        private static void Task6_AddingNewAdressAndUpdatingEmployee()
        {
            var adress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };

            Employee employee = null;

            var context = new  SoftuniContext();

            using (context)
            {
                var nakov = context.Employees
                    .Where(employeeLastName => employeeLastName.LastName == "Nakov")
                    .Take(1);

                foreach (var nakovName in nakov)
                {
                    employee = nakovName;
                }

                context.Addresses.Add(adress);
                context.SaveChanges();

                var adreesId = context.Addresses
                    .Where(c => c.AddressText == "Vitoshka 15")
                    .Select(b => b.AddressID)
                    .Take(1);

                int nakovAdressId = 0;

                foreach (var id in adreesId)
                {
                    nakovAdressId = id;
                }

                employee.AddressID = nakovAdressId;

                context.SaveChanges();

                var output = context.Employees
                    .OrderByDescending(c => c.AddressID)
                    .Select(b => b.Address.AddressText)
                    .Take(10);

                foreach (var line in output)
                {
                    Console.WriteLine(line);
                }
            }
        }

        private static void Task5_EmployeesFromResearchAndDevelopment()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var employees = context.Employees
                    .Where(employee => employee.Department.Name == "Research and Development")
                    .OrderBy(employee => employee.Salary)
                    .ThenByDescending(employee => employee.FirstName);

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} " +
                        $"from {employee.Department.Name} - ${employee.Salary:F2}");
                }
            }
        }

        private static void Task4_EmployeesWithSalaryOver50000()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var employeesNames = context.Employees
                    .Where(employee => employee.Salary > 50000)
                    .Select(employee => employee.FirstName);

                foreach (string employeeName in employeesNames)
                {
                    Console.WriteLine(employeeName);
                }
            }
        }

        private static void Task3_EmployeesFullInformation()
        {
            var context = new SoftuniContext();

            using (context)
            {
                var emplyoeesFirstNames = context.Employees
                    .Select(employee => employee.FirstName)
                    .ToList();

                var emplyoeesLastNames = context.Employees
                    .Select(employee => employee.LastName)
                    .ToList();

                var emplyoeesMiddleNames = context.Employees
                    .Select(employee => employee.MiddleName)
                    .ToList();

                var emplyoeesJobTitles = context.Employees
                    .Select(employee => employee.JobTitle)
                    .ToList();

                var emplyoeesSalarys = context.Employees
                    .Select(employee => employee.Salary)
                    .ToList();

                var employeeCounter = emplyoeesFirstNames.Count;
                var outputArray = new string[employeeCounter];

                for (int i = 0; i < employeeCounter; i++)
                {
                    outputArray[i] = emplyoeesFirstNames[i]
                                    + ' '
                                    + emplyoeesLastNames[i]
                                    + ' '
                                    + emplyoeesMiddleNames[i]
                                    + ' '
                                    + emplyoeesJobTitles[i]
                                    + ' '
                                    + emplyoeesSalarys[i];
                }

                foreach (var employee in outputArray)
                {
                    Console.WriteLine(employee);
                }

            }
        }
    }
}
