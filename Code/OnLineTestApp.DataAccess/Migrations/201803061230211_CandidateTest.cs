namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateTestDetails",
                c => new
                    {
                        TestDetailId = c.Guid(nullable: false),
                        CandidateId = c.Guid(nullable: false),
                        CandidateTestDetailId = c.Guid(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ReferenceNumber = c.String(),
                        TotalQuestions = c.Int(nullable: false),
                        TotalAnsweredQuestions = c.Int(nullable: false),
                        TotalCorrectQuestions = c.Int(nullable: false),
                        TotalWrongQuestions = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        TotalMarksObtained = c.Int(nullable: false),
                        TotalMarks = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateTestDetailId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId)
                .ForeignKey("dbo.TestDetails", t => t.TestDetailId)
                .Index(t => t.TestDetailId, unique: true)
                .Index(t => t.CandidateId, unique: true);
            
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
                        FkCandidateTestDetailId = c.Guid(nullable: false),
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
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        FkCreatedBy = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        TotalOptions = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateTestQuestionId)
                .ForeignKey("dbo.CandidateTestDetails", t => t.FkCandidateTestDetailId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.QuestionFieldTypes", t => t.FieldType)
                .Index(t => t.FkCandidateTestDetailId)
                .Index(t => t.FieldType)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateTestQuestionOptions", "FkQuestionId", "dbo.CandidateTestQuestions");
            DropForeignKey("dbo.CandidateTestQuestions", "FieldType", "dbo.QuestionFieldTypes");
            DropForeignKey("dbo.CandidateTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.CandidateTestQuestions", "FkCandidateTestDetailId", "dbo.CandidateTestDetails");
            DropForeignKey("dbo.CandidateTestDetails", "TestDetailId", "dbo.TestDetails");
            DropForeignKey("dbo.CandidateTestDetails", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkCreatedBy" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FieldType" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkCandidateTestDetailId" });
            DropIndex("dbo.CandidateTestQuestionOptions", new[] { "FkQuestionId" });
            DropIndex("dbo.CandidateTestDetails", new[] { "CandidateId" });
            DropIndex("dbo.CandidateTestDetails", new[] { "TestDetailId" });
            DropTable("dbo.CandidateTestQuestions");
            DropTable("dbo.CandidateTestQuestionOptions");
            DropTable("dbo.CandidateTestDetails");
        }
    }
}
