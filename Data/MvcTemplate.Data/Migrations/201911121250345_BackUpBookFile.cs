namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackUpBookFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BackUpFile", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BackUpFile");
        }
    }
}
