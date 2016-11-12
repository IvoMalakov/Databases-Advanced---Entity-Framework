using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GringottsDB.Models
{
    public class WizardDeposit
    {
        [Range(0, Int32.MaxValue), Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [MaxLength(60), Required]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [Range(0, Int32.MaxValue), Required]
        public int Age { get; set; }

        [MaxLength(100)]
        public string MagicWandCreator { get; set; }

        [Range(1, 32767)]
        public int MagicWandSize { get; set; }

        [MaxLength(20)]
        public string DepositGroup { get; set; }

        public DateTime DepositStartDate { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DepositInterests { get; set; }

        public double DepositCharge { get; set; }

        public DateTime DepositExpirationDate { get; set; }

        public bool IsDepositExpired { get; set; }
    }
}
