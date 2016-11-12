using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public enum RoomTypes
    {
        Small, Medium, Large
    }

    public class RoomType
    {
        [Key]
        public RoomTypes Type { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
