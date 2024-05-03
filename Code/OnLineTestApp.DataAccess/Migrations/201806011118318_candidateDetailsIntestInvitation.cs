namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class candidateDetailsIntestInvitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestInvitations", "CandidateName", c => c.String());
            AddColumn("dbo.TestInvitations", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestInvitations", "Mobile");
            DropColumn("dbo.TestInvitations", "CandidateName");
        }
    }
}
