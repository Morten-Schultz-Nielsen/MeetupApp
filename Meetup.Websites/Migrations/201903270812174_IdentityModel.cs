namespace Meetup.Websites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InfoId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Test", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "InfoId");
        }
    }
}
