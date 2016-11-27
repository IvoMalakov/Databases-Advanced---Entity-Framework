namespace PhotoShare.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Interfaces;

    internal class Configuration : DbMigrationsConfiguration<PhotoShareContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        //protected override void Seed(PhotoShareContext context)
        //{
        //    //TODO Seed some data in the database
        //    IUnitOfWork unit = new UnitOfWork();
        //    unit.Tags.Add(new Tag() { Name = "#Summer123" });
        //    unit.Tags.Add(new Tag() { Name = "#NYE2017aaa" });
        //    unit.Tags.Add(new Tag() { Name = "#NoMakeUp321" });
        //    Town bs = new Town() { Name = "Burgas", Country = "Bulgaria" };
        //    Town vn = new Town() { Name = "Varna", Country = "Bulgaria" };
        //    Town tr = new Town() { Name = "Turin", Country = "Italy" };
        //    unit.Towns.Add(bs);
        //    unit.Towns.Add(vn);
        //    unit.Towns.Add(tr);
        //    unit.Users.Add(new User() { Username = "pesho", Password = "Pesho?123", Email = "pesho@peshov.bg", Age = 20, RegisteredOn = DateTime.Now, LastTimeLoggedIn = DateTime.Now, BornTown = bs, CurrentTown = vn, IsDeleted = false, });
        //    unit.Users.Add(new User() { Username = "gosho", Password = "Gosho?321", Email = "pesho@peshov.bg", Age = 20, RegisteredOn = DateTime.Now, LastTimeLoggedIn = DateTime.Now, BornTown = bs, CurrentTown = vn, IsDeleted = false, });
        //    unit.Users.Add(new User() { Username = "minka", Password = "Minka?456", Email = "pesho@peshov.bg", Age = 20, RegisteredOn = DateTime.Now, LastTimeLoggedIn = DateTime.Now, BornTown = bs, CurrentTown = vn, IsDeleted = false, });
        //    //TODO seed albums
        //    //TODO seed album roles
        //    //TODO seed pictures

        //    unit.Commit();
        //}
    }
}
