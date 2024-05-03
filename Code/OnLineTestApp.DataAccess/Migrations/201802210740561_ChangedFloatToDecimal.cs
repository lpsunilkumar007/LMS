namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedFloatToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Questions", "MaxScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.QuestionOptions", "QuestionAnswerScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionOptions", "QuestionAnswerScore", c => c.Single(nullable: false));
            AlterColumn("dbo.Questions", "MaxScore", c => c.Single(nullable: false));
            AlterColumn("dbo.Questions", "TotalScore", c => c.Single(nullable: false));
        }
    }
}
