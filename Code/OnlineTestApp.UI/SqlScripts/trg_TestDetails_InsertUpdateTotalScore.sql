IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_TestDetails_InsertUpdateTotalScore]'))
BEGIN
DROP Trigger [dbo].[trg_TestDetails_InsertUpdateTotalScore] 
END
GO

CREATE TRIGGER [dbo].[trg_TestDetails_InsertUpdateTotalScore]
       ON [dbo].[TestQuestions]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
       SET NOCOUNT ON;
	   	DECLARE @TestDetailId	UNIQUEIDENTIFIER

		 IF EXISTS (Select * from INSERTED)
			BEGIN
				SELECT @TestDetailId=FkTestDetailId FROM INSERTED i;
			END
		ELSE IF EXISTS(Select * from DELETED)
			BEGIN
				SELECT @TestDetailId=FkTestDetailId FROM DELETED i;
			END

			
       UPDATE TD 
			SET TD.TotalScore=T.TotalScoreSum,
			TD.MaxScore=T.MaxScoreSum			
		FROM TestDetails TD
		INNER JOIN (SELECT SUM(TotalScore) AS TotalScoreSum,SUM(MaxScore) AS MaxScoreSum, FkTestDetailId  FROM TestQuestions WHERE IsDeleted=0 AND FkTestDetailId=@TestDetailId
		 GROUP BY FkTestDetailId) T 
		 ON T.FkTestDetailId=TD.TestDetailId
END
GO
