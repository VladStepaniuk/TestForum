namespace TestForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "TopicId", c => c.Int(nullable: false));
            AddColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.TopicModels", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.PostModels", "TopicId");
            AddForeignKey("dbo.PostModels", "TopicId", "dbo.TopicModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostModels", "TopicId", "dbo.TopicModels");
            DropIndex("dbo.PostModels", new[] { "TopicId" });
            DropColumn("dbo.TopicModels", "AuthorId");
            DropColumn("dbo.PostModels", "AuthorId");
            DropColumn("dbo.PostModels", "TopicId");
        }
    }
}
