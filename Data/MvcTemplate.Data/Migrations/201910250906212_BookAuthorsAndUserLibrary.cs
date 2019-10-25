namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BookAuthorsAndUserLibrary : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.ApplicationUserBooks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Book_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Book_Id);

            this.AddColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            this.CreateIndex("dbo.Books", "AuthorId");
            this.AddForeignKey("dbo.Books", "AuthorId", "dbo.BookAuthors", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.ApplicationUserBooks", "Book_Id", "dbo.Books");
            this.DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUser_Id", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Books", "AuthorId", "dbo.BookAuthors");
            this.DropIndex("dbo.ApplicationUserBooks", new[] { "Book_Id" });
            this.DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUser_Id" });
            this.DropIndex("dbo.Books", new[] { "AuthorId" });
            this.DropColumn("dbo.Books", "AuthorId");
            this.DropTable("dbo.ApplicationUserBooks");
            this.DropTable("dbo.BookAuthors");
        }
    }
}
