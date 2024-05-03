namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateAssignedTests",
                c => new
                    {
                        CandidateId = c.Guid(nullable: false),
                        TestDetailId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CandidateId, t.TestDetailId })
                .ForeignKey("dbo.Candidates", t => t.CandidateId)
                .ForeignKey("dbo.TestDetails", t => t.TestDetailId)
                .Index(t => t.CandidateId)
                .Index(t => t.TestDetailId);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Guid(nullable: false),
                        CandidateName = c.String(maxLength: 50),
                        CandidateEmailAddress = c.String(nullable: false, maxLength: 256),
                        FkCreatedBy = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .Index(t => t.CandidateEmailAddress, unique: true, name: "IX_Unique_Candidates_CandidateEmailAddress")
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.TestDetails",
                c => new
                    {
                        TestDetailId = c.Guid(nullable: false),
                        TestTitle = c.String(nullable: false, maxLength: 500),
                        TestReferenceNumber = c.String(maxLength: 256),
                        TestIntroductoryText = c.String(),
                        TotalTime = c.Int(nullable: false),
                        ValidForDays = c.Short(nullable: false),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FkCreatedBy = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TestDetailId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .Index(t => t.TestReferenceNumber, unique: true, name: "IX_Unique_TestDetails_TestReferenceNumber")
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.TestQuestions",
                c => new
                    {
                        TestQuestionId = c.Guid(nullable: false),
                        FkTestDetailId = c.Guid(nullable: false),
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
                        FkCreatedBy = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        TotalOptions = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TestQuestionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.QuestionFieldTypes", t => t.FieldType)
                .ForeignKey("dbo.TestDetails", t => t.FkTestDetailId)
                .Index(t => t.FkTestDetailId)
                .Index(t => t.FieldType)
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.TestQuestionOptions",
                c => new
                    {
                        QuestionOptionId = c.Guid(nullable: false),
                        FkQuestionId = c.Guid(nullable: false),
                        QuestionAnswer = c.String(),
                        QuestionAnswerScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCorrect = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionOptionId)
                .ForeignKey("dbo.TestQuestions", t => t.FkQuestionId)
                .Index(t => t.FkQuestionId);
            
            CreateTable(
                "dbo.TestTechnologies",
                c => new
                    {
                        FkTestDetailId = c.Guid(nullable: false),
                        FkQuestionTechnology = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FkTestDetailId, t.FkQuestionTechnology })
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkQuestionTechnology)
                .ForeignKey("dbo.TestDetails", t => t.FkTestDetailId)
                .Index(t => t.FkTestDetailId)
                .Index(t => t.FkQuestionTechnology);
            
            CreateTable(
                "dbo.TestLevels",
                c => new
                    {
                        FkTestDetailId = c.Guid(nullable: false),
                        FkTestQuestionLevel = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FkTestDetailId, t.FkTestQuestionLevel })
                .ForeignKey("dbo.TestDetails", t => t.FkTestDetailId)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkTestQuestionLevel)
                .Index(t => t.FkTestDetailId)
                .Index(t => t.FkTestQuestionLevel);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateAssignedTests", "TestDetailId", "dbo.TestDetails");
            DropForeignKey("dbo.TestLevels", "FkTestQuestionLevel", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.TestLevels", "FkTestDetailId", "dbo.TestDetails");
            DropForeignKey("dbo.TestTechnologies", "FkTestDetailId", "dbo.TestDetails");
            DropForeignKey("dbo.TestTechnologies", "FkQuestionTechnology", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.TestQuestions", "FkTestDetailId", "dbo.TestDetails");
            DropForeignKey("dbo.TestQuestions", "FieldType", "dbo.QuestionFieldTypes");
            DropForeignKey("dbo.TestQuestionOptions", "FkQuestionId", "dbo.TestQuestions");
            DropForeignKey("dbo.TestQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TestDetails", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.CandidateAssignedTests", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Candidates", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.TestLevels", new[] { "FkTestQuestionLevel" });
            DropIndex("dbo.TestLevels", new[] { "FkTestDetailId" });
            DropIndex("dbo.TestTechnologies", new[] { "FkQuestionTechnology" });
            DropIndex("dbo.TestTechnologies", new[] { "FkTestDetailId" });
            DropIndex("dbo.TestQuestionOptions", new[] { "FkQuestionId" });
            DropIndex("dbo.TestQuestions", new[] { "FkCreatedBy" });
            DropIndex("dbo.TestQuestions", new[] { "FieldType" });
            DropIndex("dbo.TestQuestions", new[] { "FkTestDetailId" });
            DropIndex("dbo.TestDetails", new[] { "FkCreatedBy" });
            DropIndex("dbo.TestDetails", "IX_Unique_TestDetails_TestReferenceNumber");
            DropIndex("dbo.Candidates", new[] { "FkCreatedBy" });
            DropIndex("dbo.Candidates", "IX_Unique_Candidates_CandidateEmailAddress");
            DropIndex("dbo.CandidateAssignedTests", new[] { "TestDetailId" });
            DropIndex("dbo.CandidateAssignedTests", new[] { "CandidateId" });
            DropTable("dbo.TestLevels");
            DropTable("dbo.TestTechnologies");
            DropTable("dbo.TestQuestionOptions");
            DropTable("dbo.TestQuestions");
            DropTable("dbo.TestDetails");
            DropTable("dbo.Candidates");
            DropTable("dbo.CandidateAssignedTests");
        }
    }
}
