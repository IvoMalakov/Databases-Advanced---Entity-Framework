namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ModelValidations;

    public enum ContentType
    {
        Application, Pdf, Zip
    }

    [Table("Homeworks")]
    public class Homework
    {
        private string content;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content
        {
            get { return this.content; }

            set
            {
                if (!ValidationClass.CheckIfContentIsValid(value))
                {
                    throw new ArgumentException("Content must be between 3 and 10000 symblos long");
                }

                this.content = value;
            }
        }

        [Required]
        public ContentType ContentType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public int? CourseId { get; set; }

        public int? StudentId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}
