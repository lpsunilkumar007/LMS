namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelTestQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CanSkipQuestion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "CanSkipQuestion");
        }
    }
}
