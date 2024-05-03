namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateTestDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestDetails", "TotalCorrectAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TotalWrongAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TotalPartialAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TotalCorrectPartialAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestQuestionOptions", "IsCandidateAnswered", c => c.Boolean(nullable: false));
            AddColumn("dbo.CandidateTestQuestions", "IsFullyCorrectAnswered", c => c.Boolean(nullable: false));
            AddColumn("dbo.CandidateTestQuestions", "IsPartiallyCorrectAnswered", c => c.Boolean(nullable: false));
            AddColumn("dbo.CandidateTestQuestions", "CandidateMarksObtained", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestQuestions", "IsQuestionAnswered", c => c.Boolean(nullable: false));
            DropColumn("dbo.CandidateTestDetails", "TotalCorrectQuestions");
            DropColumn("dbo.CandidateTestDetails", "TotalWrongQuestions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateTestDetails", "TotalWrongQuestions", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TotalCorrectQuestions", c => c.Int(nullable: false));
            DropColumn("dbo.CandidateTestQuestions", "IsQuestionAnswered");
            DropColumn("dbo.CandidateTestQuestions", "CandidateMarksObtained");
            DropColumn("dbo.CandidateTestQuestions", "IsPartiallyCorrectAnswered");
            DropColumn("dbo.CandidateTestQuestions", "IsFullyCorrectAnswered");
            DropColumn("dbo.CandidateTestQuestionOptions", "IsCandidateAnswered");
            DropColumn("dbo.CandidateTestDetails", "TotalCorrectPartialAnswers");
            DropColumn("dbo.CandidateTestDetails", "TotalPartialAnswers");
            DropColumn("dbo.CandidateTestDetails", "TotalWrongAnswers");
            DropColumn("dbo.CandidateTestDetails", "TotalCorrectAnswers");
        }
    }
}
