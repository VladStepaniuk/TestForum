namespace TestForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Topic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopicModels", "AuthorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TopicModels", "AuthorName");
        }
    }
}
