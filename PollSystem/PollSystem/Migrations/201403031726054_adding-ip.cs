namespace PollSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "UserIpAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "UserIpAddress");
        }
    }
}
