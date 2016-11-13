namespace Hospital.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Visitation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MinLength(3), MaxLength(10000)]
        public string Comments { get; set; }

        [Required]
        public Doctor Doctor { get; set; }

        [Required]
        public Patient Patient { get; set; }
    }
}
