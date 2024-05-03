namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailTemplateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        EmailTemplateId = c.Guid(nullable: false),
                        EmailTemplateName = c.String(nullable: false, maxLength: 500, unicode: false),
                        EmailSubject = c.String(nullable: false, maxLength: 2000, unicode: false),
                        EmailFromName = c.String(maxLength: 256, unicode: false),
                        EmailFromEmailAddress = c.String(maxLength: 100, unicode: false),
                        ReplyToName = c.String(maxLength: 100, unicode: false),
                        ReplyToEmailAddress = c.String(maxLength: 100, unicode: false),
                        EmailTemplateDescription = c.String(maxLength: 3000, unicode: false),
                        EmailBody = c.String(nullable: false),
                        FkCreatedBy = c.Guid(nullable: false),
                        EmailTemplateCode = c.Int(),
                        IsSystemTemplate = c.Boolean(nullable: false),
                        IsSharedTemplate = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmailTemplateId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailTemplates", "FkCreatedBy", "dbo.ApplicationUsers");
            DropIndex("dbo.EmailTemplates", new[] { "FkCreatedBy" });
            DropTable("dbo.EmailTemplates");
        }
    }
}
