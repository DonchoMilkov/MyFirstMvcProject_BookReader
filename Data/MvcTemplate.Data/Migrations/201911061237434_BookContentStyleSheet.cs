namespace MvcTemplate.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookContentStyleSheet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookContents", "StylePage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookContents", "StylePage", c => c.String());
        }
    }
}
