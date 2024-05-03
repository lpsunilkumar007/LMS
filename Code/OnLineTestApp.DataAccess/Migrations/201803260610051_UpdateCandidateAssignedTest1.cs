namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateAssignedTest1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateAssignedTests", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CandidateAssignedTests", "DeletedDateDateTime", c => c.DateTime());
            AddColumn("dbo.CandidateAssignedTests", "FkDeletedBy", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateAssignedTests", "FkDeletedBy");
            DropColumn("dbo.CandidateAssignedTests", "DeletedDateDateTime");
            DropColumn("dbo.CandidateAssignedTests", "IsDeleted");
        }
    }
}
