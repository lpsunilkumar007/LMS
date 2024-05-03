namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTestDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestDetails", "TotalCandidates", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestDetails", "TotalCandidates");
        }
    }
}
