namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbMajorChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "CanSkipQuestion");
            DropColumn("dbo.Questions", "ErrorMessage");
            DropColumn("dbo.Questions", "RegularExpression");
            DropColumn("dbo.Questions", "ErrorMessageRegularExpression");
            DropColumn("dbo.Questions", "ValidExtensions");
            DropColumn("dbo.Questions", "ErrorExtensions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ErrorExtensions", c => c.String(maxLength: 300));
            AddColumn("dbo.Questions", "ValidExtensions", c => c.String(maxLength: 300));
            AddColumn("dbo.Questions", "ErrorMessageRegularExpression", c => c.String(maxLength: 300));
            AddColumn("dbo.Questions", "RegularExpression", c => c.String(maxLength: 300));
            AddColumn("dbo.Questions", "ErrorMessage", c => c.String(maxLength: 300));
            AddColumn("dbo.Questions", "CanSkipQuestion", c => c.Boolean(nullable: false));
        }
    }
}
