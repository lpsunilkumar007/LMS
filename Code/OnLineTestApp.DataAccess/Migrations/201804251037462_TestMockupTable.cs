namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestMockupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SampleTestMockups",
                c => new
                    {
                        SampleTestMockUpId = c.Guid(nullable: false),
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
                .PrimaryKey(t => t.SampleTestMockUpId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkTestLevel)
                .Index(t => t.FkTestLevel)
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.SampleTestTechnologies",
                c => new
                    {
                        SampleTestTechnologyId = c.Guid(nullable: false),
                        FkSampleTestMockUpId = c.Guid(nullable: false),
                        FkTechnology = c.Guid(nullable: false),
                        FkCreatedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SampleTestTechnologyId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.SampleTestMockups", t => t.FkSampleTestMockUpId)
                .ForeignKey("dbo.LookUpDomainValues", t => t.FkTechnology)
                .Index(t => t.FkSampleTestMockUpId)
                .Index(t => t.FkTechnology)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SampleTestTechnologies", "FkTechnology", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.SampleTestTechnologies", "FkSampleTestMockUpId", "dbo.SampleTestMockups");
            DropForeignKey("dbo.SampleTestTechnologies", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.SampleTestMockups", "FkTestLevel", "dbo.LookUpDomainValues");
            DropForeignKey("dbo.SampleTestMockups", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.SampleTestTechnologies", new[] { "FkCreatedBy" });
            DropIndex("dbo.SampleTestTechnologies", new[] { "FkTechnology" });
            DropIndex("dbo.SampleTestTechnologies", new[] { "FkSampleTestMockUpId" });
            DropIndex("dbo.SampleTestMockups", new[] { "FkCreatedBy" });
            DropIndex("dbo.SampleTestMockups", new[] { "FkTestLevel" });
            DropTable("dbo.SampleTestTechnologies");
            DropTable("dbo.SampleTestMockups");
        }
    }
}
