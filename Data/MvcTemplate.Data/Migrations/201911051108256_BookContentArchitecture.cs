namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookContentArchitecture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookContents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StyleSheet = c.String(),
                        StylePage = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.NavigationItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        HtmlPagingItemId = c.Int(nullable: false),
                        BookContentId = c.Int(nullable: false),
                        Chapter = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookContents", t => t.BookContentId, cascadeDelete: true)
                .ForeignKey("dbo.HtmlPagingItems", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.BookContentId);
            
            CreateTable(
                "dbo.HtmlPagingItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageKey = c.String(),
                        HtmlContent = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        BookContent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookContents", t => t.BookContent_Id)
                .Index(t => t.BookContent_Id);
            
            AddColumn("dbo.BookAuthors", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookAuthors", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.BookAuthorBooks", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookAuthorBooks", "ModifiedOn", c => c.DateTime());
            DropColumn("dbo.Books", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Content", c => c.String());
            DropForeignKey("dbo.NavigationItems", "Id", "dbo.HtmlPagingItems");
            DropForeignKey("dbo.HtmlPagingItems", "BookContent_Id", "dbo.BookContents");
            DropForeignKey("dbo.NavigationItems", "BookContentId", "dbo.BookContents");
            DropForeignKey("dbo.BookContents", "Id", "dbo.Books");
            DropIndex("dbo.HtmlPagingItems", new[] { "BookContent_Id" });
            DropIndex("dbo.NavigationItems", new[] { "BookContentId" });
            DropIndex("dbo.NavigationItems", new[] { "Id" });
            DropIndex("dbo.BookContents", new[] { "Id" });
            DropColumn("dbo.BookAuthorBooks", "ModifiedOn");
            DropColumn("dbo.BookAuthorBooks", "CreatedOn");
            DropColumn("dbo.BookAuthors", "ModifiedOn");
            DropColumn("dbo.BookAuthors", "CreatedOn");
            DropTable("dbo.HtmlPagingItems");
            DropTable("dbo.NavigationItems");
            DropTable("dbo.BookContents");
        }
    }
}
