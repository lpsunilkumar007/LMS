namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailLogs",
                c => new
                    {
                        EmailLogId = c.Guid(nullable: false),
                        EmailToEmailAddress = c.String(maxLength: 256, unicode: false),
                        EmailToName = c.String(maxLength: 256, unicode: false),
                        FkEmailTemplateId = c.Guid(),
                        EmailTemplateName = c.String(maxLength: 500, unicode: false),
                        EmailSubject = c.String(nullable: false, maxLength: 2000, unicode: false),
                        EmailFromName = c.String(maxLength: 256, unicode: false),
                        EmailFromEmailAddress = c.String(maxLength: 100, unicode: false),
                        ReplyToName = c.String(maxLength: 100, unicode: false),
                        ReplyToEmailAddress = c.String(maxLength: 100, unicode: false),
                        EmailBodyFileName = c.String(nullable: false, maxLength: 256, unicode: false),
                        EmailSendToCandidateId = c.Guid(),
                        EmailSendToApplicationUserId = c.Guid(),
                        IsEmailSent = c.Boolean(nullable: false),
                        EmailNotSentError = c.String(),
                        FkCreatedBy = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmailLogId)
                .ForeignKey("dbo.ApplicationUsers", t => t.EmailSendToApplicationUserId)
                .ForeignKey("dbo.Candidates", t => t.EmailSendToCandidateId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.EmailTemplates", t => t.FkEmailTemplateId)
                .Index(t => t.FkEmailTemplateId)
                .Index(t => t.EmailSendToCandidateId)
                .Index(t => t.EmailSendToApplicationUserId)
                .Index(t => t.FkCreatedBy);
            
            AddColumn("dbo.Candidates", "FKCompanyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Candidates", "FKCompanyId");
            AddForeignKey("dbo.Candidates", "FKCompanyId", "dbo.Companies", "CompanyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailLogs", "FkEmailTemplateId", "dbo.EmailTemplates");
            DropForeignKey("dbo.EmailLogs", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.EmailLogs", "EmailSendToCandidateId", "dbo.Candidates");
            DropForeignKey("dbo.EmailLogs", "EmailSendToApplicationUserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Candidates", "FKCompanyId", "dbo.Companies");
            DropIndex("dbo.EmailLogs", new[] { "FkCreatedBy" });
            DropIndex("dbo.EmailLogs", new[] { "EmailSendToApplicationUserId" });
            DropIndex("dbo.EmailLogs", new[] { "EmailSendToCandidateId" });
            DropIndex("dbo.EmailLogs", new[] { "FkEmailTemplateId" });
            DropIndex("dbo.Candidates", new[] { "FKCompanyId" });
            DropColumn("dbo.Candidates", "FKCompanyId");
            DropTable("dbo.EmailLogs");
        }
    }
}
