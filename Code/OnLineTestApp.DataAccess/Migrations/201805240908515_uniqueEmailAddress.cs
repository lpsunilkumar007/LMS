namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueEmailAddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApplicationUsers", "IX_Unique_ApplicationUsers_UserName");
            CreateIndex("dbo.ApplicationUsers", "EmailAddress", unique: true, name: "IX_Unique_ApplicationUsers_Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ApplicationUsers", "IX_Unique_ApplicationUsers_Email");
            CreateIndex("dbo.ApplicationUsers", "UserName", unique: true, name: "IX_Unique_ApplicationUsers_UserName");
        }
    }
}
