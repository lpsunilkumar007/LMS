namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInTestInvitationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "IsTestFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestInvitations", "IsTestFinished");
        }
    }
}
