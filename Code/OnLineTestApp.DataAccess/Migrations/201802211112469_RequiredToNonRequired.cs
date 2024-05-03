namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredToNonRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionOptions", "QuestionAnswer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionOptions", "QuestionAnswer", c => c.String(nullable: false));
        }
    }
}
