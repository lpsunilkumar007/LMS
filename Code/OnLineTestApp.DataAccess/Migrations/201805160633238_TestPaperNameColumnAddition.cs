namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestPaperNameColumnAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "TestName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestInvitations", "TestName");
        }
    }
}
