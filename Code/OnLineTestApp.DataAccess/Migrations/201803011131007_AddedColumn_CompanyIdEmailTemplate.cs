namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumn_CompanyIdEmailTemplate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailTemplates", "FkAssignedCompanyId", c => c.Guid());
            CreateIndex("dbo.EmailTemplates", "FkAssignedCompanyId");
            AddForeignKey("dbo.EmailTemplates", "FkAssignedCompanyId", "dbo.Companies", "CompanyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailTemplates", "FkAssignedCompanyId", "dbo.Companies");
            DropIndex("dbo.EmailTemplates", new[] { "FkAssignedCompanyId" });
            DropColumn("dbo.EmailTemplates", "FkAssignedCompanyId");
        }
    }
}
