namespace PollSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Poll_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.Poll_Id)
                .Index(t => t.Poll_Id);
            
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerId = c.Int(nullable: false),
                        DateVoted = c.DateTime(nullable: false),
                        Poll_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.Poll_Id)
                .Index(t => t.Poll_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Poll_Id", "dbo.Polls");
            DropForeignKey("dbo.Answers", "Poll_Id", "dbo.Polls");
            DropIndex("dbo.Votes", new[] { "Poll_Id" });
            DropIndex("dbo.Answers", new[] { "Poll_Id" });
            DropTable("dbo.Votes");
            DropTable("dbo.Polls");
            DropTable("dbo.Answers");
        }
    }
}
