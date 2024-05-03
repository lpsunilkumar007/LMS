namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FkQuestionTechnologyIdAddedInCandiateQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestQuestions", "FkQuestionTechnologyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.CandidateTestQuestions", "FkQuestionTechnologyId");
            AddForeignKey("dbo.CandidateTestQuestions", "FkQuestionTechnologyId", "dbo.LookUpDomainValues", "LookUpDomainValueId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateTestQuestions", "FkQuestionTechnologyId", "dbo.LookUpDomainValues");
            DropIndex("dbo.CandidateTestQuestions", new[] { "FkQuestionTechnologyId" });
            DropColumn("dbo.CandidateTestQuestions", "FkQuestionTechnologyId");
        }
    }
}
