namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthorBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.BookId, t.AuthorId })
                .ForeignKey("dbo.BookAuthors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.ApplicationUserBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        UpToPage = c.Int(),
                        Rate = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.BookId, t.ApplicationUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ApplicationUserId);
            
            AddColumn("dbo.Books", "Language", c => c.String(maxLength: 100));
            AddColumn("dbo.Books", "Raiting", c => c.Double());
            AddColumn("dbo.Books", "Cover", c => c.Binary());
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.BookCategories", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthorBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.ApplicationUserBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookAuthorBooks", "AuthorId", "dbo.BookAuthors");
            DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUserId" });
            DropIndex("dbo.ApplicationUserBooks", new[] { "BookId" });
            DropIndex("dbo.BookAuthorBooks", new[] { "AuthorId" });
            DropIndex("dbo.BookAuthorBooks", new[] { "BookId" });
            AlterColumn("dbo.BookCategories", "Name", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            DropColumn("dbo.Books", "Cover");
            DropColumn("dbo.Books", "Raiting");
            DropColumn("dbo.Books", "Language");
            DropTable("dbo.ApplicationUserBooks");
            DropTable("dbo.BookAuthorBooks");
            DropTable("dbo.BookAuthors");
        }
    }
}
