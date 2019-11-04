namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IAuditInfoContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookContents", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookContents", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.BookContents", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookContents", "DeletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookContents", "DeletedOn");
            DropColumn("dbo.BookContents", "IsDeleted");
            DropColumn("dbo.BookContents", "ModifiedOn");
            DropColumn("dbo.BookContents", "CreatedOn");
        }
    }
}
