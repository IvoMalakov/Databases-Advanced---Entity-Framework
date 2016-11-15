namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using ModelValidations;

    public class Student
    {
        private string name;
        private string phoneNumber;
        private DateTime registeredOn;
        private DateTime? birthDay;
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks;

        public Student()
        {
            this.courses = new HashSet<Course>();
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

        public string PhoneNumber
        {
            get { return this.phoneNumber; }

            set
            {
                if (!ValidationClass.CheckIfPhoneNumberIsValid(value))
                {
                    throw new ArgumentException("The phone number should be between 8 and 13 symbols long.");
                }

                this.phoneNumber = value;
            }
        }

        [Required]
        public DateTime RegisteredOn
        {
            get { return this.registeredOn; }

            set
            {
                if (!ValidationClass.CheckIfRegistrationDateIsVaild(value))
                {
                    throw new ArgumentException("Registration Date should be before today");
                }

                this.registeredOn = value;
            }
        }

        public DateTime? BirthDay
        {
            get { return this.birthDay; }

            set
            {
                if (!ValidationClass.CheckIfBirthDateIsValid(value))
                {
                    throw new ArgumentException("You can not be born after today");
                }

                this.birthDay = value;
            }
        }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "This collection can not be null");
                }

                this.courses = value;
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