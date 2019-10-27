namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class LanguageRaitingCover : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Books", "Language", c => c.String());
            this.AddColumn("dbo.Books", "Raiting", c => c.Double());
            this.AddColumn("dbo.Books", "Cover", c => c.Binary());
        }

        public override void Down()
        {
            this.DropColumn("dbo.Books", "Cover");
            this.DropColumn("dbo.Books", "Raiting");
            this.DropColumn("dbo.Books", "Language");
        }
    }
}
