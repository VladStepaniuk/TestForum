namespace TestForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "AuthorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostModels", "AuthorName");
        }
    }
}
