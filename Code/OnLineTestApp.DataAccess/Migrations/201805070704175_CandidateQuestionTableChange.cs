namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateQuestionTableChange : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CandidateTestQuestions", new[] { "TestPapers_TestPaperId" });
            DropColumn("dbo.CandidateTestQuestions", "FkTestPaperId");
            RenameColumn(table: "dbo.CandidateTestQuestions", name: "TestPapers_TestPaperId", newName: "FkTestPaperId");
            AddColumn("dbo.CandidateTestQuestions", "FkTestInvitationId", c => c.Guid(nullable: false));
            AlterColumn("dbo.CandidateTestQuestions", "FkTestPaperId", c => c.Guid(nullable: false));
            CreateIndex("dbo.CandidateTestQuestions", "FkTestInvitationId");
            CreateIndex("dbo.CandidateTestQuestions", "FkTestPaperId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkTestInvitationId", "dbo.TestInvitations", "TestInvitationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateTestQuestions", "FkTestInvitationId", "dbo.TestInvitations");
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkTestPaperId" });
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkTestInvitationId" });
            AlterColumn("dbo.CandidateTestQuestions", "FkTestPaperId", c => c.Guid());
            DropColumn("dbo.CandidateTestQuestions", "FkTestInvitationId");
            RenameColumn(table: "dbo.CandidateTestQuestions", name: "FkTestPaperId", newName: "TestPapers_TestPaperId");
            AddColumn("dbo.CandidateTestQuestions", "FkTestPaperId", c => c.Guid(nullable: false));
            CreateIndex("dbo.CandidateTestQuestions", "TestPapers_TestPaperId");
        }
    }
}
