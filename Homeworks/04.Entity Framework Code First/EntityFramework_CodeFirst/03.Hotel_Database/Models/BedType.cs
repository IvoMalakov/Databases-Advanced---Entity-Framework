using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public enum BedTypes
    {
        Small, Medium, Large, KingSize
    }

    public class BedType
    {
        [Key]
        public BedTypes Type { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
