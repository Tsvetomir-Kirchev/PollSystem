namespace PollSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listofip : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Polls", "UserIpAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Polls", "UserIpAddress", c => c.String());
        }
    }
}
