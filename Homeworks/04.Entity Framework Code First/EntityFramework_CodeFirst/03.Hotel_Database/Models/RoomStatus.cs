using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public enum RoomStatuses
    {
        Occupied, Free, BeingCleaned
    }

    public class RoomStatus
    {
        [Key]
        public RoomStatuses Status { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
