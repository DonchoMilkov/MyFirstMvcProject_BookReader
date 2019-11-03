namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ContentNavigationPages : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.BookContents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StyleSheet = c.String(),
                        StylePage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Id)
                .Index(t => t.Id);

            this.CreateTable(
                "dbo.NavigationItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Chapter = c.String(),
                        BookContent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HtmlPagingItems", t => t.Id)
                .ForeignKey("dbo.BookContents", t => t.BookContent_Id)
                .Index(t => t.Id)
                .Index(t => t.BookContent_Id);

            this.CreateTable(
                "dbo.HtmlPagingItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageKey = c.String(),
                        HtmlContent = c.String(),
                        NavigationItemId = c.Int(nullable: false),
                        BookContentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookContents", t => t.BookContentId, cascadeDelete: true)
                .Index(t => t.BookContentId);

            this.AddColumn("dbo.Books", "BookContentId", c => c.Int(nullable: false));
            this.DropColumn("dbo.Books", "Content");
        }

        public override void Down()
        {
            this.AddColumn("dbo.Books", "Content", c => c.String());
            this.DropForeignKey("dbo.NavigationItems", "BookContent_Id", "dbo.BookContents");
            this.DropForeignKey("dbo.NavigationItems", "Id", "dbo.HtmlPagingItems");
            this.DropForeignKey("dbo.HtmlPagingItems", "BookContentId", "dbo.BookContents");
            this.DropForeignKey("dbo.BookContents", "Id", "dbo.Books");
            this.DropIndex("dbo.HtmlPagingItems", new[] { "BookContentId" });
            this.DropIndex("dbo.NavigationItems", new[] { "BookContent_Id" });
            this.DropIndex("dbo.NavigationItems", new[] { "Id" });
            this.DropIndex("dbo.BookContents", new[] { "Id" });
            this.DropColumn("dbo.Books", "BookContentId");
            this.DropTable("dbo.HtmlPagingItems");
            this.DropTable("dbo.NavigationItems");
            this.DropTable("dbo.BookContents");
        }
    }
}
