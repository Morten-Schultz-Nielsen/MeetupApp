namespace Meetup.Websites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Test", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Test");
        }
    }
}
