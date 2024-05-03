namespace OnlineTestApp.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddQuestionsTotalOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "TotalOptions", c => c.Int(nullable: false));            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "TotalOptions");
        }

        
    }
}
