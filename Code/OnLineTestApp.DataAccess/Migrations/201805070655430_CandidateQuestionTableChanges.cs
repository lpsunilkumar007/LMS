namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateQuestionTableChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestQuestions", "FkTestPaperId", c => c.Guid(nullable: false));
            AddColumn("dbo.CandidateTestQuestions", "TestPapers_TestPaperId", c => c.Guid());
            CreateIndex("dbo.CandidateTestQuestions", "TestPapers_TestPaperId");
            AddForeignKey("dbo.CandidateTestQuestions", "TestPapers_TestPaperId", "dbo.TestPapers", "TestPaperId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateTestQuestions", "TestPapers_TestPaperId", "dbo.TestPapers");
            DropIndex("dbo.CandidateTestQuestions", new[] { "TestPapers_TestPaperId" });
            DropColumn("dbo.CandidateTestQuestions", "TestPapers_TestPaperId");
            DropColumn("dbo.CandidateTestQuestions", "FkTestPaperId");
        }
    }
}
