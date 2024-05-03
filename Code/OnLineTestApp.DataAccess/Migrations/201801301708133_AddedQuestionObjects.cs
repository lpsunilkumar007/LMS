namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuestionObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionFieldTypes",
                c => new
                    {
                        FieldType = c.Int(nullable: false),
                        FieldDisplayName = c.String(nullable: false, maxLength: 256),
                        DisplayOrder = c.Short(nullable: false),
                        ErrorMessageRequired = c.String(nullable: false, maxLength: 300),
                        RegularExpression = c.String(maxLength: 300),
                        ErrorMessageRegularExpression = c.String(maxLength: 300),
                        ValidExtensions = c.String(maxLength: 300),
                        ErrorExtensions = c.String(maxLength: 300),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FieldType);
            
            CreateTable(
                "dbo.QuestionLevels",
                c => new
                    {
                        FkQuestionId = c.Guid(nullable: false),
                        FkQuestionLevel = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FkQuestionId, t.FkQuestionLevel })
                .ForeignKey("dbo.Questions", t => t.FkQuestionId)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkQuestionLevel)
                .Index(t => t.FkQuestionId)
                .Index(t => t.FkQuestionLevel);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Guid(nullable: false),
                        FieldType = c.Int(nullable: false),
                        TotalScore = c.Single(nullable: false),
                        QuestionTitle = c.String(nullable: false),
                        QuestionDescription = c.String(nullable: false),
                        TotalTime = c.Short(nullable: false),
                        CanSkipQuestion = c.Boolean(nullable: false),
                        MaxScore = c.Single(nullable: false),
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
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.QuestionFieldTypes", t => t.FieldType)
                .Index(t => t.FieldType)
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        QuestionOptionId = c.Guid(nullable: false),
                        FkQuestionId = c.Guid(nullable: false),
                        QuestionAnswer = c.String(nullable: false),
                        QuestionAnswerScore = c.Single(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionOptionId)
                .ForeignKey("dbo.Questions", t => t.FkQuestionId)
                .Index(t => t.FkQuestionId);
            
            CreateTable(
                "dbo.QuestionTechnologies",
                c => new
                    {
                        FkQuestionId = c.Guid(nullable: false),
                        FkQuestionTechnology = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FkQuestionId, t.FkQuestionTechnology })
                .ForeignKey("dbo.Questions", t => t.FkQuestionId)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkQuestionTechnology)
                .Index(t => t.FkQuestionId)
                .Index(t => t.FkQuestionTechnology);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionLevels", "FkQuestionLevel", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.QuestionLevels", "FkQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "FieldType", "dbo.QuestionFieldTypes");
            DropForeignKey("dbo.QuestionTechnologies", "FkQuestionTechnology", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.QuestionTechnologies", "FkQuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionOptions", "FkQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.QuestionTechnologies", new[] { "FkQuestionTechnology" });
            DropIndex("dbo.QuestionTechnologies", new[] { "FkQuestionId" });
            DropIndex("dbo.QuestionOptions", new[] { "FkQuestionId" });
            DropIndex("dbo.Questions", new[] { "FkCreatedBy" });
            DropIndex("dbo.Questions", new[] { "FieldType" });
            DropIndex("dbo.QuestionLevels", new[] { "FkQuestionLevel" });
            DropIndex("dbo.QuestionLevels", new[] { "FkQuestionId" });
            DropTable("dbo.QuestionTechnologies");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionLevels");
            DropTable("dbo.QuestionFieldTypes");
        }
    }
}
