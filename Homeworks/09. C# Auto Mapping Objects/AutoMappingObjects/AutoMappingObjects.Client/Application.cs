namespace AutoMappingObjects.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Models;
    using Models.DTOs;
    using Data;

    class Application
    {
        static void Main()
        {         
            CompanyContext context = new CompanyContext();

            //Task 1 - Simple Mapping
            SimpleMapping(context);

            //Task 2 - Advanced Mapping
            AdvancedMapping(context);

            //Task3 - Projection
            Projection(context);
        }

        private static void Projection(CompanyContext context)
        {
            ConfigureProjectionMapping();

            var employeesDtos = context.Employees
                .Where(employee => employee.BirthDay.Value.Year < 1990)
                .OrderByDescending(employee => employee.Salary)
                .ProjectTo<EmployeeDto2>();

            foreach (EmployeeDto2 employeeDto in employeesDtos)
            {
                Console.WriteLine(employeeDto.ToString());
            }
        }

        private static void ConfigureProjectionMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Employee, EmployeeDto2>()
                    .ForMember(dto => dto.ManagerLastName,
                        configurationExpress => configurationExpress.MapFrom(employee => employee.Manager.LastName));
            });
        }

        private static void AdvancedMapping(CompanyContext context)
        {
            ConfigureAdvancedMapping();

            IList<Employee> employeesList = new List<Employee>();

            var employee1 = new Employee()
            {
                FirstName = "Maria",
                LastName = "Kamenova",
                Salary = 700m,
                Address = "Dragalevci",
                BirthDay = new DateTime(1990, 08, 08),
                OnHoliday = true
            };

            var employee2 = new Employee()
            {
                FirstName = "Ivan",
                LastName = "Penkov",
                Salary = 2000m,
                BirthDay = new DateTime(1980, 01, 01),
                OnHoliday = false,
                Address = "Lulin 10",
            };

            employee1.Manager = employee2;
            employee2.Employees.Add(employee1);

            var employee3 = new Employee()
            {
                FirstName = "Dragan",
                LastName = "Draganov",
                Salary = 1700m,
                Address = "Germany",
                BirthDay = new DateTime(1930, 07, 07),
                OnHoliday = false
            };

            var employee4 = new Employee()
            {
                FirstName = "Boiko",
                LastName = "Borisov",
                Salary = 2000000000m,
                BirthDay = new DateTime(1959, 06, 13),
                OnHoliday = true,
                Address = "Bankia 1"
            };

            employee3.Manager = employee4;
            employee4.Employees.Add(employee3);

            var employee5 = new Employee()
            {
                FirstName = "Cvetan",
                LastName = "Cvetanov",
                Salary = 2000000m,
                BirthDay = new DateTime(1965, 04, 08),
                OnHoliday = true,
                Address = "Bankia 2"
            };

            employee5.Manager = employee4;
            employee4.Employees.Add(employee5);

            employeesList.Add(employee1);
            employeesList.Add(employee2);
            employeesList.Add(employee3);
            employeesList.Add(employee4);
            employeesList.Add(employee5);

            IList<ManagerDto> managerDtos = new List<ManagerDto>();

            managerDtos = Mapper.Map<IList<ManagerDto>>(employeesList);

            foreach (ManagerDto manager in managerDtos)
            {
                Console.WriteLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.EmployeesCount}");

                var employeesDtos = Mapper.Map<IEnumerable<EmployeeDto>>(manager.Employees);

                foreach (EmployeeDto employee in employeesDtos)
                {
                    Console.WriteLine($"- {employee.FirstName} {employee.LastName} {employee.Salary}");
                }
            }

            context.Employees.AddRange(employeesList);
            context.SaveChanges();
        }

        private static void ConfigureAdvancedMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Employee, EmployeeDto>();
                config.CreateMap<Employee, ManagerDto>()
                    .ForMember(dto => dto.Employees,
                        configurationExpress => configurationExpress.MapFrom(manager => manager.Employees))
                    .ForMember(dto => dto.EmployeesCount,
                        configurationExpress => configurationExpress.MapFrom(manager => manager.Employees.Count));
            });
        }

        private static void SimpleMapping(CompanyContext context)
        {
            ConfigureSimpleMapping();

            Employee pesho = new Employee()
            {
                FirstName = "Petar",
                LastName = "Ivanov",
                Salary = 1000m,
                Address = "Tintiava 15-17 Sofia",
                BirthDay = DateTime.Now
            };

            EmployeeDto peshoDto = Mapper.Map<EmployeeDto>(pesho);

            Console.WriteLine("Employee Fist Name: {0}, Employee Last Name: {1}, Employee Salary: {2}",
                peshoDto.FirstName, peshoDto.LastName, peshoDto.Salary);

            context.Employees.Add(pesho);
            context.SaveChanges();
        }

        private static void ConfigureSimpleMapping()
        {
            Mapper.Initialize(config => config.CreateMap<Employee, EmployeeDto>());
        }
    }
}
