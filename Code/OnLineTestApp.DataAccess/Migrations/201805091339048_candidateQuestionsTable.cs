namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class candidateQuestionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CandidateTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkCreatedBy" });
            AlterColumn("dbo.CandidateTestQuestions", "FkCreatedBy", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CandidateTestQuestions", "FkCreatedBy", c => c.Guid(nullable: false));
            CreateIndex("dbo.CandidateTestQuestions", "FkCreatedBy");
            AddForeignKey("dbo.CandidateTestQuestions", "FkCreatedBy", "dbo.ApplicationUsers", "ApplicationUserId");
        }
    }
}
