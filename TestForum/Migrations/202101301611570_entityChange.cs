namespace TestForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entityChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostModels", "Topic_Id", "dbo.TopicModels");
            DropIndex("dbo.PostModels", new[] { "Topic_Id" });
            RenameColumn(table: "dbo.PostModels", name: "Topic_Id", newName: "TopicId");
            RenameColumn(table: "dbo.TopicModels", name: "Author_Id", newName: "AuthorId");
            RenameIndex(table: "dbo.TopicModels", name: "IX_Author_Id", newName: "IX_AuthorId");
            AddColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.PostModels", "TopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.PostModels", "TopicId");
            AddForeignKey("dbo.PostModels", "TopicId", "dbo.TopicModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostModels", "TopicId", "dbo.TopicModels");
            DropIndex("dbo.PostModels", new[] { "TopicId" });
            AlterColumn("dbo.PostModels", "TopicId", c => c.Int());
            DropColumn("dbo.PostModels", "AuthorId");
            RenameIndex(table: "dbo.TopicModels", name: "IX_AuthorId", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.TopicModels", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.PostModels", name: "TopicId", newName: "Topic_Id");
            CreateIndex("dbo.PostModels", "Topic_Id");
            AddForeignKey("dbo.PostModels", "Topic_Id", "dbo.TopicModels", "Id");
        }
    }
}
