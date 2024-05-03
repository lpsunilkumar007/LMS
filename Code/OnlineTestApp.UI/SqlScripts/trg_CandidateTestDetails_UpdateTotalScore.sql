IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_CandidateTestDetails_UpdateTotalScore]'))
BEGIN
DROP Trigger [dbo].[trg_CandidateTestDetails_UpdateTotalScore] 
END
GO

CREATE TRIGGER [dbo].[trg_CandidateTestDetails_UpdateTotalScore]
       ON [dbo].[CandidateTestQuestions]
AFTER UPDATE
AS
BEGIN
       SET NOCOUNT ON;
	   	DECLARE @CandidateTestDetailId	UNIQUEIDENTIFIER

		 SELECT @CandidateTestDetailId=FkCandidateTestDetailId FROM INSERTED i;
		
			
       UPDATE TD 
			SET TD.TotalScore=T.TotalScoreSum,
			TD.MaxScore=T.MaxScoreSum			
		FROM CandidateTestDetails TD
		INNER JOIN (SELECT SUM(TotalScore) AS TotalScoreSum,SUM(MaxScore) AS MaxScoreSum, FkCandidateTestDetailId  FROM CandidateTestQuestions WHERE IsDeleted=0 AND FkCandidateTestDetailId=@CandidateTestDetailId
		 GROUP BY FkCandidateTestDetailId) T 
		 ON T.FkCandidateTestDetailId=TD.CandidateTestDetailId
END

GO