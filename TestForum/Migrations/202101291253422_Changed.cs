namespace TestForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostModels", "TopicId", "dbo.TopicModels");
            DropIndex("dbo.PostModels", new[] { "TopicId" });
            RenameColumn(table: "dbo.PostModels", name: "TopicId", newName: "Topic_Id");
            AlterColumn("dbo.PostModels", "Topic_Id", c => c.Int());
            CreateIndex("dbo.PostModels", "Topic_Id");
            AddForeignKey("dbo.PostModels", "Topic_Id", "dbo.TopicModels", "Id");
            DropColumn("dbo.PostModels", "AuthorId");
            DropColumn("dbo.TopicModels", "AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TopicModels", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PostModels", "Topic_Id", "dbo.TopicModels");
            DropIndex("dbo.PostModels", new[] { "Topic_Id" });
            AlterColumn("dbo.PostModels", "Topic_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PostModels", name: "Topic_Id", newName: "TopicId");
            CreateIndex("dbo.PostModels", "TopicId");
            AddForeignKey("dbo.PostModels", "TopicId", "dbo.TopicModels", "Id", cascadeDelete: true);
        }
    }
}
