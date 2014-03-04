namespace PollSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeipaddresstable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserIpAddresses", "PollId", "dbo.Polls");
            DropIndex("dbo.UserIpAddresses", new[] { "PollId" });
            AddColumn("dbo.Votes", "UserIp", c => c.String());
            DropTable("dbo.UserIpAddresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserIpAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        IpAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Votes", "UserIp");
            CreateIndex("dbo.UserIpAddresses", "PollId");
            AddForeignKey("dbo.UserIpAddresses", "PollId", "dbo.Polls", "Id", cascadeDelete: true);
        }
    }
}
