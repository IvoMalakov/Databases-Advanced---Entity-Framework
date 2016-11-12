namespace UserDB
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using UserDB.Models;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Town> Towns { get; set; }
    }
}