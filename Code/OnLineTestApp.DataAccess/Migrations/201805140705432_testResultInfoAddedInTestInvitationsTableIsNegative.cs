namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testResultInfoAddedInTestInvitationsTableIsNegative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "IsNegativeMarking", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestInvitations", "IsNegativeMarking");
        }
    }
}
