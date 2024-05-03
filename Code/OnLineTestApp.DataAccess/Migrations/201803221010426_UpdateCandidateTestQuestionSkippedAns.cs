namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateTestQuestionSkippedAns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestQuestions", "IsSkippedAnswered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateTestQuestions", "IsSkippedAnswered");
        }
    }
}
