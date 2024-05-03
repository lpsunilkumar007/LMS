namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateQuestionsTableChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CandidateAssignedTests", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.CandidateTestDetails", "FkCandidateAssignedTestId", "dbo.CandidateAssignedTests");
            DropForeignKey("dbo.CandidateTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.CandidateTestQuestionOptions", "FkQuestionId", "dbo.CandidateTestQuestions");
            DropForeignKey("dbo.CandidateTestQuestions", "FieldType", "dbo.QuestionFieldTypes");
            DropForeignKey("dbo.CandidateTestQuestions", "FkTestInvitationId", "dbo.TestInvitations");
            DropForeignKey("dbo.CandidateTestQuestions", "FkTestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.CandidateTestQuestions", "CandidateTestDetails_FkCandidateAssignedTestId", "dbo.CandidateTestDetails");
            DropForeignKey("dbo.CandidateAssignedTests", "TestDetailId", "dbo.TestDetails");
            DropIndex("dbo.CandidateAssignedTests", new[] { "CandidateId" });
            DropIndex("dbo.CandidateAssignedTests", new[] { "TestDetailId" });
            DropIndex("dbo.CandidateAssignedTests", "IX_Unique_CandidateAssignedTest_TestReferenceNumber");
            DropIndex("dbo.CandidateTestDetails", new[] { "FkCandidateAssignedTestId" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkTestInvitationId" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkTestPaperId" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FieldType" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkCreatedBy" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "CandidateTestDetails_FkCandidateAssignedTestId" });
            DropIndex("dbo.CandidateTestQuestionOptions", new[] { "FkQuestionId" });
            DropTable("dbo.CandidateAssignedTests");
            DropTable("dbo.CandidateTestDetails");
            DropTable("dbo.CandidateTestQuestions");
            DropTable("dbo.CandidateTestQuestionOptions");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.CandidateQuestionOptionId);
            
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
                        CandidateTestDetails_FkCandidateAssignedTestId = c.Guid(),
                    })
                .PrimaryKey(t => t.CandidateTestQuestionId);
            
            CreateTable(
                "dbo.CandidateTestDetails",
                c => new
                    {
                        FkCandidateAssignedTestId = c.Guid(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        TotalQuestions = c.Int(nullable: false),
                        TotalAnsweredQuestions = c.Int(nullable: false),
                        TotalCorrectAnswers = c.Int(nullable: false),
                        TotalWrongAnswers = c.Int(nullable: false),
                        TotalPartialAnswers = c.Int(nullable: false),
                        TotalCorrectPartialAnswers = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        TotalCandidateScoreObtained = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FkCandidateAssignedTestId);
            
            CreateTable(
                "dbo.CandidateAssignedTests",
                c => new
                    {
                        CandidateId = c.Guid(nullable: false),
                        TestDetailId = c.Guid(nullable: false),
                        CandidateAssignedTestId = c.Guid(nullable: false),
                        TestReferenceNumber = c.String(nullable: false, maxLength: 256, unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateAssignedTestId);
            
            CreateIndex("dbo.CandidateTestQuestionOptions", "FkQuestionId");
            CreateIndex("dbo.CandidateTestQuestions", "CandidateTestDetails_FkCandidateAssignedTestId");
            CreateIndex("dbo.CandidateTestQuestions", "FkCreatedBy");
            CreateIndex("dbo.CandidateTestQuestions", "FieldType");
            CreateIndex("dbo.CandidateTestQuestions", "FkTestPaperId");
            CreateIndex("dbo.CandidateTestQuestions", "FkTestInvitationId");
            CreateIndex("dbo.CandidateTestDetails", "FkCandidateAssignedTestId");
            CreateIndex("dbo.CandidateAssignedTests", "TestReferenceNumber", unique: true, name: "IX_Unique_CandidateAssignedTest_TestReferenceNumber");
            CreateIndex("dbo.CandidateAssignedTests", "TestDetailId", unique: true);
            CreateIndex("dbo.CandidateAssignedTests", "CandidateId", unique: true);
            AddForeignKey("dbo.CandidateAssignedTests", "TestDetailId", "dbo.TestDetails", "TestDetailId");
            AddForeignKey("dbo.CandidateTestQuestions", "CandidateTestDetails_FkCandidateAssignedTestId", "dbo.CandidateTestDetails", "FkCandidateAssignedTestId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkTestPaperId", "dbo.TestPapers", "TestPaperId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkTestInvitationId", "dbo.TestInvitations", "TestInvitationId");
            AddForeignKey("dbo.CandidateTestQuestions", "FieldType", "dbo.QuestionFieldTypes", "FieldType");
            AddForeignKey("dbo.CandidateTestQuestionOptions", "FkQuestionId", "dbo.CandidateTestQuestions", "CandidateTestQuestionId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers", "ApplicationUserId");
            AddForeignKey("dbo.CandidateTestDetails", "FkCandidateAssignedTestId", "dbo.CandidateAssignedTests", "CandidateAssignedTestId");
            AddForeignKey("dbo.CandidateAssignedTests", "CandidateId", "dbo.Candidates", "CandidateId");
        }
    }
}
