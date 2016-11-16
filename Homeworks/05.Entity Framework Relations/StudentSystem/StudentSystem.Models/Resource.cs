namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using ModelValidations;

    public enum ResorceType
    {
        Video, Presentation, Document, Other
    }

    public class Resource
    {
        private string name;
        private string url;
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
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

        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses
        {
            get { return this.licenses; }

            set { this.licenses = value; }
        }
    }
}
