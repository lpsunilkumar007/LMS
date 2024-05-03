namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserSettingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserSettings",
                c => new
                    {
                        ApplicationUserId = c.Guid(nullable: false),
                        IsMenuOpen = c.Boolean(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserSettings", "ApplicationUserId", "dbo.ApplicationUsers");
            DropIndex("dbo.ApplicationUserSettings", new[] { "ApplicationUserId" });
            DropTable("dbo.ApplicationUserSettings");
        }
    }
}
