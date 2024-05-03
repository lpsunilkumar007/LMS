namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTestReferenceNumber : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TestDetails", "IX_Unique_TestDetails_TestReferenceNumber");
            AddColumn("dbo.CandidateAssignedTests", "TestReferenceNumber", c => c.String(nullable: false, maxLength: 256, unicode: false));
            CreateIndex("dbo.CandidateAssignedTests", "TestReferenceNumber", unique: true, name: "IX_Unique_CandidateAssignedTest_TestReferenceNumber");
            DropColumn("dbo.TestDetails", "TestReferenceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestDetails", "TestReferenceNumber", c => c.String(maxLength: 256));
            DropIndex("dbo.CandidateAssignedTests", "IX_Unique_CandidateAssignedTest_TestReferenceNumber");
            DropColumn("dbo.CandidateAssignedTests", "TestReferenceNumber");
            CreateIndex("dbo.TestDetails", "TestReferenceNumber", unique: true, name: "IX_Unique_TestDetails_TestReferenceNumber");
        }
    }
}
