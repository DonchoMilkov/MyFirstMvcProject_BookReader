namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FormatRestraintsAndRequiredFields : DbMigration
    {
        public override void Up()
        {
            this.AlterColumn("dbo.BookAuthors", "Name", c => c.String(nullable: false, maxLength: 150));
            this.AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 150));
            this.AlterColumn("dbo.Books", "Language", c => c.String(maxLength: 100));
            this.AlterColumn("dbo.BookCategories", "Name", c => c.String(nullable: false, maxLength: 100));
        }

        public override void Down()
        {
            this.AlterColumn("dbo.BookCategories", "Name", c => c.String());
            this.AlterColumn("dbo.Books", "Language", c => c.String());
            this.AlterColumn("dbo.Books", "Title", c => c.String());
            this.AlterColumn("dbo.BookAuthors", "Name", c => c.String());
        }
    }
}
