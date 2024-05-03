namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NegativeMarkingColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestQuestions", "IsNegativeMarking", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateTestQuestions", "IsNegativeMarking");
        }
    }
}
