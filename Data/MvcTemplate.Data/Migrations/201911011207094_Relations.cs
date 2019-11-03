namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Relations : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

            this.CreateTable(
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

            this.AddColumn("dbo.Books", "Language", c => c.String(maxLength: 100));
            this.AddColumn("dbo.Books", "Raiting", c => c.Double());
            this.AddColumn("dbo.Books", "Cover", c => c.Binary());
            this.AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 150));
            this.AlterColumn("dbo.BookCategories", "Name", c => c.String(nullable: false, maxLength: 100));
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.BookAuthorBooks", "BookId", "dbo.Books");
            this.DropForeignKey("dbo.ApplicationUserBooks", "BookId", "dbo.Books");
            this.DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.BookAuthorBooks", "AuthorId", "dbo.BookAuthors");
            this.DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUserId" });
            this.DropIndex("dbo.ApplicationUserBooks", new[] { "BookId" });
            this.DropIndex("dbo.BookAuthorBooks", new[] { "AuthorId" });
            this.DropIndex("dbo.BookAuthorBooks", new[] { "BookId" });
            this.AlterColumn("dbo.BookCategories", "Name", c => c.String());
            this.AlterColumn("dbo.Books", "Title", c => c.String());
            this.DropColumn("dbo.Books", "Cover");
            this.DropColumn("dbo.Books", "Raiting");
            this.DropColumn("dbo.Books", "Language");
            this.DropTable("dbo.ApplicationUserBooks");
            this.DropTable("dbo.BookAuthorBooks");
            this.DropTable("dbo.BookAuthors");
        }
    }
}
