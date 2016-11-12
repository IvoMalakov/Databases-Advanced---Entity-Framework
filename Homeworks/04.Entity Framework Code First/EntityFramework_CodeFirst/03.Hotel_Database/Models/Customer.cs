using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Customer
    {
        [Key]
        public int AccountNumber { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(50), Required]
        public string LastName { get; set; }

        [MinLength(8), MaxLength(13), Required]
        public string PhoneNumber { get; set; }

        [MinLength(2), MaxLength(50)]
        public string EmergencyName { get; set; }

        [MinLength(8), MaxLength(13)]
        public string EmergencyNumber { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
