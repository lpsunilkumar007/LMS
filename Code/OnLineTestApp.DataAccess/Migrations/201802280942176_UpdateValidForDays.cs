namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidForDays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestDetails", "ValidUpTo", c => c.DateTime(nullable: false));
            DropColumn("dbo.TestDetails", "ValidForDays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestDetails", "ValidForDays", c => c.Short(nullable: false));
            DropColumn("dbo.TestDetails", "ValidUpTo");
        }
    }
}
