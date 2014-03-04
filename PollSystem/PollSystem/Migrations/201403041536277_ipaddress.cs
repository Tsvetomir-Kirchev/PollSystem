namespace PollSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ipaddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserIpAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        IpAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.PollId, cascadeDelete: true)
                .Index(t => t.PollId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserIpAddresses", "PollId", "dbo.Polls");
            DropIndex("dbo.UserIpAddresses", new[] { "PollId" });
            DropTable("dbo.UserIpAddresses");
        }
    }
}
