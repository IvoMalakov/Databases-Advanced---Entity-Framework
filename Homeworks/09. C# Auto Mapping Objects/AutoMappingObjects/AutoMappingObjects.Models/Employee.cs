namespace AutoMappingObjects.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class Employee
    {
        private ICollection<Employee> employees;

        public Employee()
        {
            this.employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Address { get; set; }

        public bool OnHoliday { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees
        {
            get { return this.employees; }

            set { this.employees = value; }
        }
    }
}
