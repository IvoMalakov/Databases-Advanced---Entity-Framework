namespace Hospital.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Medicament
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(50), Required]
        public string Name { get; set; }

        [Required]
        public Patient Patient { get; set; }
    }
}