namespace UserDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstNameLastNameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
