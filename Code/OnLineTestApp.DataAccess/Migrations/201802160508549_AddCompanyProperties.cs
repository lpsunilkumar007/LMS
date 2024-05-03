namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "OrgNo", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Companies", "Telephone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Companies", "EmailAddress", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "EmailAddress");
            DropColumn("dbo.Companies", "Telephone");
            DropColumn("dbo.Companies", "OrgNo");
        }
    }
}
