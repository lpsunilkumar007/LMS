namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempTestsTable_CreatedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TempTests", "FkCreatedBy", c => c.Guid(nullable: false));
            CreateIndex("dbo.TempTests", "FkCreatedBy");
            AddForeignKey("dbo.TempTests", "FkCreatedBy", "dbo.ApplicationUsers", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TempTests", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.TempTests", new[] { "FkCreatedBy" });
            DropColumn("dbo.TempTests", "FkCreatedBy");
        }
    }
}
