namespace AutoMappingObjects.Models.DTOs
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        private ICollection<Employee> employees;

        public ManagerDto()
        {
            this.employees = new HashSet<Employee>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeesCount { get; set; }

        public virtual ICollection<Employee> Employees
        {
            get { return this.employees; }

            set { this.employees = value; }
        }
    }
}
