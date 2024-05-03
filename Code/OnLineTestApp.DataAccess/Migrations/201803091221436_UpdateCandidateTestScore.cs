namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateTestScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestDetails", "TotalCandidateScoreObtained", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CandidateTestQuestions", "TotalCandidateScoreObtained", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CandidateTestDetails", "TotalScoreObtained");
            DropColumn("dbo.CandidateTestQuestions", "CandidateMarksObtained");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateTestQuestions", "CandidateMarksObtained", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TotalScoreObtained", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CandidateTestQuestions", "TotalCandidateScoreObtained");
            DropColumn("dbo.CandidateTestDetails", "TotalCandidateScoreObtained");
        }
    }
}
