using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string LastName { get; set; }

        [MinLength(2), MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
