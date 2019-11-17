namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.ApplicationUserBooks", "CreatedOn", c => c.DateTime(nullable: false));
            this.AddColumn("dbo.ApplicationUserBooks", "ModifiedOn", c => c.DateTime());
        }

        public override void Down()
        {
            this.DropColumn("dbo.ApplicationUserBooks", "ModifiedOn");
            this.DropColumn("dbo.ApplicationUserBooks", "CreatedOn");
        }
    }
}
