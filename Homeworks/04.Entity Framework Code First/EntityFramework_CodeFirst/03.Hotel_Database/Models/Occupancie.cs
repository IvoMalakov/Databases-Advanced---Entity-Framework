using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Occupancie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateOccupied { get; set; }

        [Range(0, int.MaxValue)]
        public int AccountNumber { get; set; }

        [Range(0, 1000)]
        public int RoomNumber { get; set; }

        [Range(typeof(decimal), "0", "1000000000")]
        public decimal RateApplied { get; set; }

        [Range(typeof(decimal), "0", "1000000000")]
        public decimal PhoneCharge { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
