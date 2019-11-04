namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IAuditInfoIDeletableEntities1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookAuthorBooks", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookAuthorBooks", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookAuthorBooks", "ModifiedOn");
            DropColumn("dbo.BookAuthorBooks", "CreatedOn");
        }
    }
}
