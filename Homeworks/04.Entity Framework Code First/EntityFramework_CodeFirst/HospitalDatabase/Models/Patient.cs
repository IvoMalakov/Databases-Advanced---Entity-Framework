namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Patient
    {
        private string email;

        public Patient()
        {
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();
            this.Medicaments = new HashSet<Medicament>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string LastName { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Email
        {
            get { return this.email; }

            set
            {
                if (!EmailIsValid(value))
                {
                    throw new ArgumentException("Error! Email is not in valid format");
                }

                this.email = value;
            }
        }

        public DateTime DateOfBirth { get; set; }

        public byte[] Picture { get; set; }

        public bool HasMedicalInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }
    }
}
