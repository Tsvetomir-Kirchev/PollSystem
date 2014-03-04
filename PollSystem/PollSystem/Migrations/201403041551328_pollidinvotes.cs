namespace PollSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pollidinvotes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Poll_Id", "dbo.Polls");
            DropIndex("dbo.Votes", new[] { "Poll_Id" });
            RenameColumn(table: "dbo.Votes", name: "Poll_Id", newName: "PollId");
            AlterColumn("dbo.Votes", "PollId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "PollId");
            AddForeignKey("dbo.Votes", "PollId", "dbo.Polls", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "PollId", "dbo.Polls");
            DropIndex("dbo.Votes", new[] { "PollId" });
            AlterColumn("dbo.Votes", "PollId", c => c.Int());
            RenameColumn(table: "dbo.Votes", name: "PollId", newName: "Poll_Id");
            CreateIndex("dbo.Votes", "Poll_Id");
            AddForeignKey("dbo.Votes", "Poll_Id", "dbo.Polls", "Id");
        }
    }
}
