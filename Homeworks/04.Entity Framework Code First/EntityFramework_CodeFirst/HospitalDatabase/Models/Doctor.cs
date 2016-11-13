namespace Hospital.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class Doctor
    {
        public Doctor()
        {
            this.Visitations = new HashSet<Visitation>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(50), Required]
        public string Name { get; set; }

        [MinLength(3), MaxLength(50), Required]
        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
    }
}