namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ApplicationUserBooksDeletable : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.ApplicationUserBooks", "IsDeleted", c => c.Boolean(nullable: false));
            this.AddColumn("dbo.ApplicationUserBooks", "DeletedOn", c => c.DateTime());
        }

        public override void Down()
        {
            this.DropColumn("dbo.ApplicationUserBooks", "DeletedOn");
            this.DropColumn("dbo.ApplicationUserBooks", "IsDeleted");
        }
    }
}
