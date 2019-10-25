#pragma warning disable SA1412 // Store files as UTF-8 with byte order mark
namespace MvcTemplate.Data.Migrations
#pragma warning restore SA1412 // Store files as UTF-8 with byte order mark
{
    using System.Data.Entity.Migrations;

    public partial class BooksAndBooksCategories : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Books", "CategoryId", "dbo.BookCategories");
            this.DropIndex("dbo.BookCategories", new[] { "IsDeleted" });
            this.DropIndex("dbo.Books", new[] { "IsDeleted" });
            this.DropIndex("dbo.Books", new[] { "CategoryId" });
            this.DropTable("dbo.BookCategories");
            this.DropTable("dbo.Books");
        }
    }
}
