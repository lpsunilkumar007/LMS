namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateTest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CandidateTestQuestions", "StartTime", c => c.DateTime());
            AlterColumn("dbo.CandidateTestQuestions", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CandidateTestQuestions", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CandidateTestQuestions", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
