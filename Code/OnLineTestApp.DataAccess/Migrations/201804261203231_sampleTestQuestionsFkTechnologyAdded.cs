namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sampleTestQuestionsFkTechnologyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SampleTestQuestions", "FkQuestionTechnologyId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SampleTestQuestions", "FkQuestionTechnologyId");
        }
    }
}
