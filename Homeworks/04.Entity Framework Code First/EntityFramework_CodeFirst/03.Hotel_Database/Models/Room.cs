using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        public BedType BedType { get; set; }

        [Range(typeof(decimal), "0", "10")]
        public decimal Rate { get; set; }

        [Required]
        public RoomStatus RoomStatus { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
