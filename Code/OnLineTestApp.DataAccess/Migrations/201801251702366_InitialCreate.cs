namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        UserRoleId = c.Short(nullable: false, identity: true),
                        UserRoleName = c.String(nullable: false, maxLength: 256, unicode: false),
                        UserRoleCode = c.Int(nullable: false),
                        UserRoleDescription = c.String(nullable: false, maxLength: 2000, unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .Index(t => t.UserRoleName, unique: true, name: "IX_Unique_ApplicationUserRoles_UserRoleName")
                .Index(t => t.UserRoleCode, unique: true, name: "IX_Unique_ApplicationUserRoles_UserRoleCode");
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        ApplicationUserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        UserPassword = c.String(nullable: false, maxLength: 256),
                        SocialMediaUniqueId = c.String(maxLength: 500, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 256),
                        LastName = c.String(nullable: false, maxLength: 256),
                        //FullName = c.String(),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        UserSocialMediaData = c.String(maxLength: 8000, unicode: false),
                        MobileNumber = c.String(nullable: false, maxLength: 50),
                        AlternateNumber = c.String(maxLength: 100),
                        UserRegistrationType = c.Int(nullable: false),
                        IsSystemUser = c.Boolean(nullable: false),
                        FkCreatedBy = c.Guid(),
                        FkCompanyId = c.Guid(nullable: false),
                        FkUserRoleId = c.Short(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.ApplicationUserRoles", t => t.FkUserRoleId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.Companies", t => t.FkCompanyId)
                .Index(t => t.UserName, unique: true, name: "IX_Unique_ApplicationUsers_UserName")
                .Index(t => t.FkCreatedBy)
                .Index(t => t.FkCompanyId)
                .Index(t => t.FkUserRoleId);
            Sql("ALTER TABLE ApplicationUsers ADD FullName as FirstName + ' ' + LastName");
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 256),
                        CanDisplayCompany = c.Boolean(nullable: false),
                        FkCreatedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.DataChangeLogs",
                c => new
                    {
                        DataChangeLogId = c.Guid(nullable: false),
                        OriginalValue = c.String(nullable: false),
                        NewValue = c.String(nullable: false),
                        RecordId = c.String(nullable: false),
                        EventType = c.String(nullable: false, maxLength: 256),
                        Model = c.String(nullable: false, maxLength: 256),
                        FkCreatedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DataChangeLogId);
            
            CreateTable(
                "dbo.LookUpDomains",
                c => new
                    {
                        LookUpDomainId = c.Guid(nullable: false),
                        LookUpDomainCode = c.Int(nullable: false),
                        LookUpDomainDescription = c.String(nullable: false, maxLength: 1000, unicode: false),
                        FkCreatedBy = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LookUpDomainId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .Index(t => t.LookUpDomainCode, unique: true, name: "IX_Unique_LookUpDomain_LookUpDomainCode")
                .Index(t => t.FkCreatedBy);
            
            CreateTable(
                "dbo.LookUpDomainValues",
                c => new
                    {
                        LookUpDomainValueId = c.Guid(nullable: false),
                        FkLookUpDomainId = c.Guid(nullable: false),
                        LookUpDomainCode = c.String(nullable: false, maxLength: 500, unicode: false),
                        LookUpDomainValue = c.String(nullable: false),
                        LookUpDomainValueText = c.String(nullable: false),
                        CanEditLookUpDomainValue = c.Boolean(nullable: false),
                        CanEditLookUpDomainValueText = c.Boolean(nullable: false),
                        CanDeleteRecord = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        FkCreatedBy = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DeletedDateDateTime = c.DateTime(),
                        FkDeletedBy = c.Guid(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LookUpDomainValueId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FkCreatedBy)
                .ForeignKey("dbo.LookUpDomains", t => t.FkLookUpDomainId)
                .Index(t => t.FkLookUpDomainId)
                .Index(t => t.FkCreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LookUpDomainValues", "FkLookUpDomainId", "dbo.LookUpDomains");
            DropForeignKey("dbo.LookUpDomainValues", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.LookUpDomains", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "FkCompanyId", "dbo.Companies");
            DropForeignKey("dbo.ApplicationUsers", "FkCreatedBy", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "FkUserRoleId", "dbo.ApplicationUserRoles");
            DropIndex("dbo.LookUpDomainValues", new[] { "FkCreatedBy" });
            DropIndex("dbo.LookUpDomainValues", new[] { "FkLookUpDomainId" });
            DropIndex("dbo.LookUpDomains", new[] { "FkCreatedBy" });
            DropIndex("dbo.LookUpDomains", "IX_Unique_LookUpDomain_LookUpDomainCode");
            DropIndex("dbo.ApplicationUsers", new[] { "FkUserRoleId" });
            DropIndex("dbo.ApplicationUsers", new[] { "FkCompanyId" });
            DropIndex("dbo.ApplicationUsers", new[] { "FkCreatedBy" });
            DropIndex("dbo.ApplicationUsers", "IX_Unique_ApplicationUsers_UserName");
            DropIndex("dbo.ApplicationUserRoles", "IX_Unique_ApplicationUserRoles_UserRoleCode");
            DropIndex("dbo.ApplicationUserRoles", "IX_Unique_ApplicationUserRoles_UserRoleName");
            DropTable("dbo.LookUpDomainValues");
            DropTable("dbo.LookUpDomains");
            DropTable("dbo.DataChangeLogs");
            DropTable("dbo.Companies");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserRoles");
        }
    }
}
