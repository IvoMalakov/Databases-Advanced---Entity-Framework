namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ModelValidations;

    public enum ResorceType
    {
        Video, Presentation, Document, Other
    }

    public class Resource
    {
        private string name;
        private string url;

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
        public ResorceType ResorceType { get; set; }

        [Required]
        public string Url
        {
            get { return this.url; }

            set
            {
                if (!ValidationClass.CheckIfUrlIsValid(value))
                {
                    throw new ArgumentException("Url is not in valid format");
                }

                this.url = value;
            }
        }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
