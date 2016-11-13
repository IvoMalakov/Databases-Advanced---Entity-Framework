namespace Hospital.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Diagnose
    {
        [Key]
        public int Id { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string Name { get; set; }

        [MinLength(3), MaxLength(10000)]
        public string Comments { get; set; }

        [Required]
        public Patient Patient { get; set; }
    }
}
