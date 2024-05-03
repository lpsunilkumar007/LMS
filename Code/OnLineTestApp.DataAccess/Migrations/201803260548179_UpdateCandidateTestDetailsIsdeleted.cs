namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCandidateTestDetailsIsdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateTestDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CandidateTestDetails", "DeletedDateDateTime", c => c.DateTime());
            AddColumn("dbo.CandidateTestDetails", "FkDeletedBy", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateTestDetails", "FkDeletedBy");
            DropColumn("dbo.CandidateTestDetails", "DeletedDateDateTime");
            DropColumn("dbo.CandidateTestDetails", "IsDeleted");
        }
    }
}
