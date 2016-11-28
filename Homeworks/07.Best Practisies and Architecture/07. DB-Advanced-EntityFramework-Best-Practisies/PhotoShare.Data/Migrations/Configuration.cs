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

        //    User pesho = new User()
        //    {
        //        Username = "pesho",
        //        Password = "Pesho?123",
        //        Email = "pesho@peshov.bg",
        //        Age = 20,
        //        RegisteredOn = DateTime.Now,
        //        LastTimeLoggedIn = DateTime.Now,
        //        BornTown = bs,
        //        CurrentTown = vn,
        //        IsDeleted = false
        //    };

        //    User gosho = new User()
        //    {
        //        Username = "gosho",
        //        Password = "Gosho?321",
        //        Email = "pesho@peshov.bg",
        //        Age = 20,
        //        RegisteredOn = DateTime.Now,
        //        LastTimeLoggedIn = DateTime.Now,
        //        BornTown = bs,
        //        CurrentTown = vn,
        //        IsDeleted = false
        //    };

        //    User minka = new User()
        //    {
        //        Username = "minka",
        //        Password = "Minka?456",
        //        Email = "pesho@peshov.bg",
        //        Age = 20,
        //        RegisteredOn = DateTime.Now,
        //        LastTimeLoggedIn = DateTime.Now,
        //        BornTown = bs,
        //        CurrentTown = vn,
        //        IsDeleted = false
        //    };

        //    unit.Users.Add(pesho);
        //    unit.Users.Add(gosho);
        //    unit.Users.Add(minka);

        //    //TODO seed albums
        //    Album album1 = new Album() { Name = "Album1", IsPublic = true, BackgroundColor = Color.Green};
        //    Album album2 = new Album() { Name = "Album2", IsPublic = true, BackgroundColor = Color.Blue };
        //    Album album3 = new Album() { Name = "Album3", IsPublic = false, BackgroundColor = Color.Cyan };
        //    unit.Albums.Add(album1);
        //    unit.Albums.Add(album2);
        //    unit.Albums.Add(album3);

        //    //TODO seed album roles
        //    AlbumRole albumRole1 = new AlbumRole() {Role = Role.Owner, Album = album3, User = minka};
        //    AlbumRole albumRole2 = new AlbumRole() {Role = Role.Viewer, Album = album2, User = gosho};
        //    AlbumRole albumRole3 = new AlbumRole() {Role = Role.Owner, Album = album1, User = pesho};

        //    unit.AlbumRoles.Add(albumRole1);
        //    unit.AlbumRoles.Add(albumRole2);
        //    unit.AlbumRoles.Add(albumRole3);
            
        //    //TODO seed pictures
        //    Picture picture1 = new Picture() {Title = "Title1", Albums = {album1, album2}};
        //    Picture picture2 = new Picture() {Title = "Title2"};
        //    Picture picture3 = new Picture() {Title = "Title3", Albums = {album3, album2}, Path = "http://www.abv.bg"};

        //    unit.Pictures.Add(picture1);
        //    unit.Pictures.Add(picture2);
        //    unit.Pictures.Add(picture3);

        //    unit.Commit();
        //}
    }
}
