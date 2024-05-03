namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreatingCandidateQuestionsandOptionsDbTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateTestQuestionOptions",
                c => new
                    {
                        CandidateQuestionOptionId = c.Guid(nullable: false),
                        FkQuestionId = c.Guid(nullable: false),
                        QuestionAnswer = c.String(),
                        QuestionAnswerScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCorrect = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        IsAnswer = c.String(),
                        IsCandidateAnswered = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateQuestionOptionId)
                .ForeignKey("dbo.CandidateTestQuestions", t => t.FkQuestionId)
                .Index(t => t.FkQuestionId);
            
            CreateTable(
                "dbo.CandidateTestQuestions",
                c => new
                    {
                        CandidateTestQuestionId = c.Guid(nullable: false),
                        FkTestInvitationId = c.Guid(nullable: false),
                        FkTestPaperId = c.Guid(nullable: false),
                        FieldType = c.Int(nullable: false),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DisplayOrder = c.Int(nullable: false),
                        QuestionTitle = c.String(nullable: false),
                        QuestionDescription = c.String(nullable: false),
                        TotalTime = c.Short(nullable: false),
                        CanSkipQuestion = c.Boolean(nullable: false),
                        MaxScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NegativeMarks = c.Boolean(nullable: false),
                        ErrorMessage = c.String(maxLength: 300),
                        RegularExpression = c.String(maxLength: 300),
                        ErrorMessageRegularExpression = c.String(maxLength: 300),
                        ValidExtensions = c.String(maxLength: 300),
                        ErrorExtensions = c.String(maxLength: 300),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        FkCreatedBy = c.Guid(nullable: false),
                        TotalOptions = c.Int(nullable: false),
                        IsFullyCorrectAnswered = c.Boolean(nullable: false),
                        IsPartiallyCorrectAnswered = c.Boolean(nullable: false),
                        TotalCandidateScoreObtained = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsQuestionAnswered = c.Boolean(nullable: false),
                        IsSkippedAnswered = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateTestQuestionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.QuestionFieldTypes", t => t.FieldType)
                .ForeignKey("dbo.TestInvitations", t => t.FkTestInvitationId)
                .ForeignKey("dbo.TestPapers", t => t.FkTestPaperId)
                .Index(t => t.FkTestInvitationId)
                .Index(t => t.FkTestPaperId)
                .Index(t => t.FieldType)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateTestQuestionOptions", "FkQuestionId", "dbo.CandidateTestQuestions");
            DropForeignKey("dbo.CandidateTestQuestions", "FkTestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.CandidateTestQuestions", "FkTestInvitationId", "dbo.TestInvitations");
            DropForeignKey("dbo.CandidateTestQuestions", "FieldType", "dbo.QuestionFieldTypes");
            DropForeignKey("dbo.CandidateTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkCreatedBy" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FieldType" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkTestPaperId" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkTestInvitationId" });
            DropIndex("dbo.CandidateTestQuestionOptions", new[] { "FkQuestionId" });
            DropTable("dbo.CandidateTestQuestions");
            DropTable("dbo.CandidateTestQuestionOptions");
        }
    }
}
