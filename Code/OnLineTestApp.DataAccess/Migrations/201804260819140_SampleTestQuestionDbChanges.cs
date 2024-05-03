namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleTestQuestionDbChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SampleTestQuestions",
                c => new
                    {
                        SampleTestQuestionId = c.Guid(nullable: false),
                        FkQuestionId = c.Guid(),
                        FkSampleTestMockUpId = c.Guid(),
                        FkCreatedBy = c.Guid(),
                        FkModefiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SampleTestQuestionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.Questions", t => t.FkQuestionId)
                .ForeignKey("dbo.SampleTestMockups", t => t.FkSampleTestMockUpId)
                .Index(t => t.FkQuestionId)
                .Index(t => t.FkSampleTestMockUpId)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SampleTestQuestions", "FkSampleTestMockUpId", "dbo.SampleTestMockups");
            DropForeignKey("dbo.SampleTestQuestions", "FkQuestionId", "dbo.Questions");
            DropForeignKey("dbo.SampleTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.SampleTestQuestions", new[] { "FkCreatedBy" });
            DropIndex("dbo.SampleTestQuestions", new[] { "FkSampleTestMockUpId" });
            DropIndex("dbo.SampleTestQuestions", new[] { "FkQuestionId" });
            DropTable("dbo.SampleTestQuestions");
        }
    }
}
