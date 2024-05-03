namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateQuestionsDbTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkCandidateAssignedTestId" });
            RenameColumn(table: "dbo.CandidateTestQuestions", name: "FkCandidateAssignedTestId", newName: "CandidateTestDetails_FkCandidateAssignedTestId");
            AlterColumn("dbo.CandidateTestQuestions", "CandidateTestDetails_FkCandidateAssignedTestId", c => c.Guid());
            CreateIndex("dbo.CandidateTestQuestions", "CandidateTestDetails_FkCandidateAssignedTestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CandidateTestQuestions", new[] { "CandidateTestDetails_FkCandidateAssignedTestId" });
            AlterColumn("dbo.CandidateTestQuestions", "CandidateTestDetails_FkCandidateAssignedTestId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.CandidateTestQuestions", name: "CandidateTestDetails_FkCandidateAssignedTestId", newName: "FkCandidateAssignedTestId");
            CreateIndex("dbo.CandidateTestQuestions", "FkCandidateAssignedTestId");
        }
    }
}
