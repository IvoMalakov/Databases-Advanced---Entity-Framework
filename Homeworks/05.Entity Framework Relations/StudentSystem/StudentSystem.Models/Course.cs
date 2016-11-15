namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using ModelValidations;

    public class Course
    {
        private string name;
        private decimal price;
        private ICollection<Student> students;
        private ICollection<Resource> resources;
        private ICollection<Homework> homeworks;

        public Course()
        {
            this.students = new HashSet<Student>();
            this.resources = new HashSet<Resource>();
            this.homeworks = new HashSet<Homework>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name
        {
            get { return this.name; }

            set
            {
                if (!ValidationClass.CheckIfNameIsValid(value))
                {
                    throw new ArgumentException("The name should be between 3 and 50 symbols long.");
                }

                this.name = value;
            }
        }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price
        {
            get { return this.price; }

            set
            {
                if (!ValidationClass.CheckIfPriceIsValid(value))
                {
                    throw new ArgumentException("The price must be a positive number");
                }

                this.price = value;
            }
        }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "This collection can not be null");
                }

                this.students = value;
            }
        }

        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "This collection can not be null");
                }

                this.resources = value;
            }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "This collection can not be null");
                }

                this.homeworks = value;
            }
        }
    }
}
