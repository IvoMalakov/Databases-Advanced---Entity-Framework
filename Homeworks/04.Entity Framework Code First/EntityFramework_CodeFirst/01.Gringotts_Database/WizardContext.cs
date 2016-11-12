using GringottsDB.Models;

namespace GringottsDB
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WizardContext : DbContext
    {
        public WizardContext()
            : base("name=WizardContext")
        {
        }

        public IDbSet<WizardDeposit> WizardDeposits { get; set; }
    }
}