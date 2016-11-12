using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDB.Models
{
    public partial class User
    {
        private string password;
        private string email;

        [Range(0, Int32.MaxValue), Key]
        public int Id { get; set; }

        [MinLength(4), MaxLength(30), Required]
        public string Username { get; set; }

        [MaxLength(50), Required]
        public string FirstName { get; set; }

        [MaxLength(50), Required]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        [MinLength(6), MaxLength(50), Required]
        public string Password
        {
            get { return this.password; }

            set
            {
                if (!this.CheckIfLowLetterIsContained(value)
                    || !this.CheckIfUpperLetterIsContained(value)
                    || !this.CheckIfDigitIsContained(value)
                    || !this.CheckIfSpecialSymbolsIsContained(value))
                {
                    throw new ArgumentException("The password must containt at least one lowercase letter, one uppercase letter, one digit and one special symbol.");
                }

                this.password = value;
            }
        }

        [Required]
        public string Email
        {
            get { return this.email; }

            set
            {
                if (!this.EmailIsValid(value))
                {
                    throw new ArgumentException("Error! Email is not in valid format");
                }

                this.email = value;
            }
        }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public Town BornTown { get; set; }

        public Town CurrentlyLivingTown { get; set; }
    }
}
