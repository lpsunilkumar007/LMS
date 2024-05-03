namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionOptions", "DisplayOrder", c => c.Int(nullable: false));
            DropColumn("dbo.QuestionOptions", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionOptions", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.QuestionOptions", "DisplayOrder");
        }
    }
}
