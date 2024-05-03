namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PassingPersenatageColumnAddintion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "PassingPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TestPapers", "PassingPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SampleTestMockups", "PassingPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SampleTestMockups", "PassingPercentage");
            DropColumn("dbo.TestPapers", "PassingPercentage");
            DropColumn("dbo.TestInvitations", "PassingPercentage");
        }
    }
}
