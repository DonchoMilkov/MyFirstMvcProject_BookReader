namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IAuditInfoIDeletableEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookAuthors", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookAuthors", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.NavigationItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.NavigationItems", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.NavigationItems", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.NavigationItems", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.HtmlPagingItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.HtmlPagingItems", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.HtmlPagingItems", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.HtmlPagingItems", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HtmlPagingItems", "ModifiedOn");
            DropColumn("dbo.HtmlPagingItems", "CreatedOn");
            DropColumn("dbo.HtmlPagingItems", "DeletedOn");
            DropColumn("dbo.HtmlPagingItems", "IsDeleted");
            DropColumn("dbo.NavigationItems", "ModifiedOn");
            DropColumn("dbo.NavigationItems", "CreatedOn");
            DropColumn("dbo.NavigationItems", "DeletedOn");
            DropColumn("dbo.NavigationItems", "IsDeleted");
            DropColumn("dbo.BookAuthors", "ModifiedOn");
            DropColumn("dbo.BookAuthors", "CreatedOn");
        }
    }
}
