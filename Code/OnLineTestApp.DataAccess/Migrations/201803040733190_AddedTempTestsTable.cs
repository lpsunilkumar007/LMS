namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTempTestsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempTests",
                c => new
                    {
                        TempTestId = c.Guid(nullable: false),
                        QuestionIds = c.String(maxLength: 8000, unicode: false),
                        TotalTime = c.Int(nullable: false),
                        TotalQuestions = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TempTestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TempTests");
        }
    }
}
