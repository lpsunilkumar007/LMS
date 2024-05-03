namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnsChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CandidateTestDetails", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.CandidateTestDetails", "TestDetailId", "dbo.TestDetails");
            DropIndex("dbo.CandidateTestDetails", new[] { "TestDetailId" });
            DropIndex("dbo.CandidateTestDetails", new[] { "CandidateId" });
            DropIndex("dbo.CandidateTestDetails", new[] { "CandidateAssignedTestId" });
            RenameColumn(table: "dbo.CandidateTestDetails", name: "CandidateAssignedTestId", newName: "FkCandidateAssignedTestId");
            CreateIndex("dbo.CandidateTestDetails", "FkCandidateAssignedTestId", unique: true, name: "IX_Unique_CandidateTestDetails_FkCandidateAssignedTestId");
            DropColumn("dbo.CandidateTestDetails", "TestDetailId");
            DropColumn("dbo.CandidateTestDetails", "CandidateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateTestDetails", "CandidateId", c => c.Guid(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "TestDetailId", c => c.Guid(nullable: false));
            DropIndex("dbo.CandidateTestDetails", "IX_Unique_CandidateTestDetails_FkCandidateAssignedTestId");
            RenameColumn(table: "dbo.CandidateTestDetails", name: "FkCandidateAssignedTestId", newName: "CandidateAssignedTestId");
            CreateIndex("dbo.CandidateTestDetails", "CandidateAssignedTestId");
            CreateIndex("dbo.CandidateTestDetails", "CandidateId", unique: true);
            CreateIndex("dbo.CandidateTestDetails", "TestDetailId", unique: true);
            AddForeignKey("dbo.CandidateTestDetails", "TestDetailId", "dbo.TestDetails", "TestDetailId");
            AddForeignKey("dbo.CandidateTestDetails", "CandidateId", "dbo.Candidates", "CandidateId");
        }
    }
}
