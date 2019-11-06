namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BookContentPagingNavigation : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.BookContents",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        StyleSheet = c.String(),
                        StylePage = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Books", t => t.BookId)
                .Index(t => t.BookId);

            this.CreateTable(
                "dbo.NavigationItems",
                c => new
                    {
                        HtmlPagingItemId = c.Int(nullable: false),
                        Chapter = c.String(),
                        BookContentBookId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.HtmlPagingItemId)
                .ForeignKey("dbo.BookContents", t => t.BookContentBookId, cascadeDelete: true)
                .ForeignKey("dbo.HtmlPagingItems", t => t.HtmlPagingItemId)
                .Index(t => t.HtmlPagingItemId)
                .Index(t => t.BookContentBookId);

            this.CreateTable(
                "dbo.HtmlPagingItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageKey = c.String(),
                        HtmlContent = c.String(),
                        BookContentBookId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookContents", t => t.BookContentBookId, cascadeDelete: true)
                .Index(t => t.BookContentBookId);

            this.AddColumn("dbo.BookAuthors", "CreatedOn", c => c.DateTime(nullable: false));
            this.AddColumn("dbo.BookAuthors", "ModifiedOn", c => c.DateTime());
            this.AddColumn("dbo.BookAuthorBooks", "CreatedOn", c => c.DateTime(nullable: false));
            this.AddColumn("dbo.BookAuthorBooks", "ModifiedOn", c => c.DateTime());
            this.DropColumn("dbo.Books", "Content");
        }

        public override void Down()
        {
            this.AddColumn("dbo.Books", "Content", c => c.String());
            this.DropForeignKey("dbo.NavigationItems", "HtmlPagingItemId", "dbo.HtmlPagingItems");
            this.DropForeignKey("dbo.HtmlPagingItems", "BookContentBookId", "dbo.BookContents");
            this.DropForeignKey("dbo.NavigationItems", "BookContentBookId", "dbo.BookContents");
            this.DropForeignKey("dbo.BookContents", "BookId", "dbo.Books");
            this.DropIndex("dbo.HtmlPagingItems", new[] { "BookContentBookId" });
            this.DropIndex("dbo.NavigationItems", new[] { "BookContentBookId" });
            this.DropIndex("dbo.NavigationItems", new[] { "HtmlPagingItemId" });
            this.DropIndex("dbo.BookContents", new[] { "BookId" });
            this.DropColumn("dbo.BookAuthorBooks", "ModifiedOn");
            this.DropColumn("dbo.BookAuthorBooks", "CreatedOn");
            this.DropColumn("dbo.BookAuthors", "ModifiedOn");
            this.DropColumn("dbo.BookAuthors", "CreatedOn");
            this.DropTable("dbo.HtmlPagingItems");
            this.DropTable("dbo.NavigationItems");
            this.DropTable("dbo.BookContents");
        }
    }
}
