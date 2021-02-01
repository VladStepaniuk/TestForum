namespace TestForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgnoreChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostModels", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "Content", c => c.String());
        }
    }
}
