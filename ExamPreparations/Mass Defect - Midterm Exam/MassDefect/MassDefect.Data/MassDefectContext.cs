namespace MassDefect.Data
{
    using System;
    using System.Data.Entity;
    using Models;
    using System.Linq;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<Person> Persons { get; set; } 

        public virtual DbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany<Anomaly>(person => person.Anomalies)
                .WithMany(anomaly => anomaly.Victims)
                .Map(config =>
                {
                    config.MapLeftKey("PersonId");
                    config.MapRightKey("AnomalyId");
                    config.ToTable("AnomalyVictims");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}