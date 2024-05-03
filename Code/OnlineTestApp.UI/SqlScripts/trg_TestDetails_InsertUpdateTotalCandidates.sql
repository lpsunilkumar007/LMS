IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_TestDetails_InsertUpdateTotalCandidates]'))
BEGIN
DROP Trigger [dbo].[trg_TestDetails_InsertUpdateTotalCandidates] 
END
GO

CREATE TRIGGER [dbo].[trg_TestDetails_InsertUpdateTotalCandidates]
       ON [dbo].[CandidateAssignedTests]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
       SET NOCOUNT ON;
	   	DECLARE @TestDetailId	UNIQUEIDENTIFIER

		 IF EXISTS (Select * from INSERTED)
			BEGIN
				SELECT @TestDetailId=TestDetailId FROM INSERTED i;
			END
		ELSE IF EXISTS(Select * from DELETED)
			BEGIN
				SELECT @TestDetailId=TestDetailId FROM DELETED i;
			END

			
       UPDATE TD 
			SET TD.TotalCandidates=T.TotalCount			
		FROM TestDetails TD
		INNER JOIN (SELECT COUNT(CandidateId) AS TotalCount, TestDetailId  FROM CandidateAssignedTests WHERE TestDetailId=@TestDetailId
		 GROUP BY TestDetailId) T 
		 ON T.TestDetailId=TD.TestDetailId
END
GO
