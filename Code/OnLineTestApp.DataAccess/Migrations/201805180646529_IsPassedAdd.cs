namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsPassedAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "IsPassed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestInvitations", "IsPassed");
        }
    }
}
