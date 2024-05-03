namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testResultInfoAddedInTestInvitationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "TotalQuestions", c => c.Int(nullable: false));
            AddColumn("dbo.TestInvitations", "TotalAnsweredQuestions", c => c.Int(nullable: false));
            AddColumn("dbo.TestInvitations", "TotalCorrectAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.TestInvitations", "TotalWrongAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.TestInvitations", "TotalPartialAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.TestInvitations", "TotalCorrectPartialAnswers", c => c.Int(nullable: false));
            AddColumn("dbo.TestInvitations", "TotalCandidateScoreObtained", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TestInvitations", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestInvitations", "TotalScore");
            DropColumn("dbo.TestInvitations", "TotalCandidateScoreObtained");
            DropColumn("dbo.TestInvitations", "TotalCorrectPartialAnswers");
            DropColumn("dbo.TestInvitations", "TotalPartialAnswers");
            DropColumn("dbo.TestInvitations", "TotalWrongAnswers");
            DropColumn("dbo.TestInvitations", "TotalCorrectAnswers");
            DropColumn("dbo.TestInvitations", "TotalAnsweredQuestions");
            DropColumn("dbo.TestInvitations", "TotalQuestions");
        }
    }
}
