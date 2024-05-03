namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestDetails", "MaxScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CandidateTestDetails", "TotalScoreObtained", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CandidateTestDetails", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CandidateTestDetails", "MaxScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CandidateTestDetails", "TotalMarksObtained");
            DropColumn("dbo.CandidateTestDetails", "TotalMarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateTestDetails", "TotalMarks", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TotalMarksObtained", c => c.Int(nullable: false));
            DropColumn("dbo.CandidateTestDetails", "MaxScore");
            DropColumn("dbo.CandidateTestDetails", "TotalScore");
            DropColumn("dbo.CandidateTestDetails", "TotalScoreObtained");
            DropColumn("dbo.TestDetails", "MaxScore");
        }
    }
}
