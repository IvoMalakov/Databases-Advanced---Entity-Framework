using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
            Task8_FindEmployeesInPeriod();
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
