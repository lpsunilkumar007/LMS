namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateAssignedTest : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CandidateAssignedTests", new[] { "CandidateId" });
            DropIndex("dbo.CandidateAssignedTests", new[] { "TestDetailId" });
            DropPrimaryKey("dbo.CandidateAssignedTests");
            AddColumn("dbo.CandidateAssignedTests", "CandidateAssignedTestId", c => c.Guid(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "CandidateAssignedTestId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.CandidateAssignedTests", "CandidateAssignedTestId");
            CreateIndex("dbo.CandidateAssignedTests", "CandidateId", unique: true);
            CreateIndex("dbo.CandidateAssignedTests", "TestDetailId", unique: true);
            CreateIndex("dbo.CandidateTestDetails", "CandidateAssignedTestId");
            AddForeignKey("dbo.CandidateTestDetails", "CandidateAssignedTestId", "dbo.CandidateAssignedTests", "CandidateAssignedTestId");
            DropColumn("dbo.CandidateTestDetails", "ReferenceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateTestDetails", "ReferenceNumber", c => c.String());
            DropForeignKey("dbo.CandidateTestDetails", "CandidateAssignedTestId", "dbo.CandidateAssignedTests");
            DropIndex("dbo.CandidateTestDetails", new[] { "CandidateAssignedTestId" });
            DropIndex("dbo.CandidateAssignedTests", new[] { "TestDetailId" });
            DropIndex("dbo.CandidateAssignedTests", new[] { "CandidateId" });
            DropPrimaryKey("dbo.CandidateAssignedTests");
            DropColumn("dbo.CandidateTestDetails", "CandidateAssignedTestId");
            DropColumn("dbo.CandidateAssignedTests", "CandidateAssignedTestId");
            AddPrimaryKey("dbo.CandidateAssignedTests", new[] { "CandidateId", "TestDetailId" });
            CreateIndex("dbo.CandidateAssignedTests", "TestDetailId");
            CreateIndex("dbo.CandidateAssignedTests", "CandidateId");
        }
    }
}
