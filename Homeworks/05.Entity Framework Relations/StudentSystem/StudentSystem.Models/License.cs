namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ModelValidations;

   public class License
   {
       private string name;

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
   }
}
