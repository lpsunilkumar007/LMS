namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateTestDetails1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CandidateTestQuestions", "FkCandidateTestDetailId", "dbo.CandidateTestDetails");
            DropIndex("dbo.CandidateTestDetails", "IX_Unique_CandidateTestDetails_FkCandidateAssignedTestId");
            RenameColumn(table: "dbo.CandidateTestQuestions", name: "FkCandidateTestDetailId", newName: "FkCandidateAssignedTestId");
            RenameIndex(table: "dbo.CandidateTestQuestions", name: "IX_FkCandidateTestDetailId", newName: "IX_FkCandidateAssignedTestId");
            DropPrimaryKey("dbo.CandidateTestDetails");
            AddPrimaryKey("dbo.CandidateTestDetails", "FkCandidateAssignedTestId");
            CreateIndex("dbo.CandidateTestDetails", "FkCandidateAssignedTestId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkCandidateAssignedTestId", "dbo.CandidateTestDetails", "FkCandidateAssignedTestId");
            DropColumn("dbo.CandidateTestDetails", "CandidateTestDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateTestDetails", "CandidateTestDetailId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.CandidateTestQuestions", "FkCandidateAssignedTestId", "dbo.CandidateTestDetails");
            DropIndex("dbo.CandidateTestDetails", new[] { "FkCandidateAssignedTestId" });
            DropPrimaryKey("dbo.CandidateTestDetails");
            AddPrimaryKey("dbo.CandidateTestDetails", "CandidateTestDetailId");
            RenameIndex(table: "dbo.CandidateTestQuestions", name: "IX_FkCandidateAssignedTestId", newName: "IX_FkCandidateTestDetailId");
            RenameColumn(table: "dbo.CandidateTestQuestions", name: "FkCandidateAssignedTestId", newName: "FkCandidateTestDetailId");
            CreateIndex("dbo.CandidateTestDetails", "FkCandidateAssignedTestId", unique: true, name: "IX_Unique_CandidateTestDetails_FkCandidateAssignedTestId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkCandidateTestDetailId", "dbo.CandidateTestDetails", "CandidateTestDetailId");
        }
    }
}
