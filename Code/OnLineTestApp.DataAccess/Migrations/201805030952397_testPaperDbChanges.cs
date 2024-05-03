namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testPaperDbChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestInvitations",
                c => new
                    {
                        TestInvitationId = c.Guid(nullable: false),
                        FkTestPaperId = c.Guid(nullable: false),
                        IsAttempted = c.Boolean(nullable: false),
                        Email = c.String(),
                        TestTocken = c.String(),
                        FkCreatedBy = c.Guid(),
                        FkModefiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TestInvitationId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.TestPapers", t => t.FkTestPaperId)
                .Index(t => t.FkTestPaperId)
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.TestPapers",
                c => new
                    {
                        TestPaperId = c.Guid(nullable: false),
                        MockupName = c.String(),
                        SampleTestBatch = c.String(),
                        TotalQuestions = c.Int(nullable: false),
                        TotalMarks = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        IsNegativeMarking = c.Boolean(nullable: false),
                        FkTestLevel = c.Guid(nullable: false),
                        FkCreatedBy = c.Guid(),
                        FkModefiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TestPaperId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkTestLevel)
                .Index(t => t.FkTestLevel)
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.TestPaperQuestions",
                c => new
                    {
                        TestQuestionId = c.Guid(nullable: false),
                        FkQuestionId = c.Guid(),
                        FkTestPaperId = c.Guid(),
                        FkQuestionTechnologyId = c.Guid(),
                        FkCreatedBy = c.Guid(),
                        FkModefiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TestQuestionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.Questions", t => t.FkQuestionId)
                .ForeignKey("dbo.TestPapers", t => t.FkTestPaperId)
                .Index(t => t.FkQuestionId)
                .Index(t => t.FkTestPaperId)
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.TestPaperTechnologies",
                c => new
                    {
                        TestTechnologyId = c.Guid(nullable: false),
                        FkTestPaperId = c.Guid(nullable: false),
                        FkTechnology = c.Guid(nullable: false),
                        FkCreatedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.TestTechnologyId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkTechnology)
                .ForeignKey("dbo.TestPapers", t => t.FkTestPaperId)
                .Index(t => t.FkTestPaperId)
                .Index(t => t.FkTechnology)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestPaperTechnologies", "FkTestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.TestPaperTechnologies", "FkTechnology", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.TestPaperTechnologies", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TestPaperQuestions", "FkTestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.TestPaperQuestions", "FkQuestionId", "dbo.Questions");
            DropForeignKey("dbo.TestPaperQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TestInvitations", "FkTestPaperId", "dbo.TestPapers");
            DropForeignKey("dbo.TestPapers", "FkTestLevel", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.TestPapers", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TestInvitations", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.TestPaperTechnologies", new[] { "FkCreatedBy" });
            DropIndex("dbo.TestPaperTechnologies", new[] { "FkTechnology" });
            DropIndex("dbo.TestPaperTechnologies", new[] { "FkTestPaperId" });
            DropIndex("dbo.TestPaperQuestions", new[] { "FkCreatedBy" });
            DropIndex("dbo.TestPaperQuestions", new[] { "FkTestPaperId" });
            DropIndex("dbo.TestPaperQuestions", new[] { "FkQuestionId" });
            DropIndex("dbo.TestPapers", new[] { "FkCreatedBy" });
            DropIndex("dbo.TestPapers", new[] { "FkTestLevel" });
            DropIndex("dbo.TestInvitations", new[] { "FkCreatedBy" });
            DropIndex("dbo.TestInvitations", new[] { "FkTestPaperId" });
            DropTable("dbo.TestPaperTechnologies");
            DropTable("dbo.TestPaperQuestions");
            DropTable("dbo.TestPapers");
            DropTable("dbo.TestInvitations");
        }
    }
}
