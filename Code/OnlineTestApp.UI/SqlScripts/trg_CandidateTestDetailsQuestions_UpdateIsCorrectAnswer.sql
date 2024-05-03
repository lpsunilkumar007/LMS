IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_CandidateTestDetailsQuestions_UpdateIsCorrectAnswer]'))
BEGIN
DROP Trigger [dbo].[trg_CandidateTestDetailsQuestions_UpdateIsCorrectAnswer] 
END
GO

CREATE TRIGGER [dbo].[trg_CandidateTestDetailsQuestions_UpdateIsCorrectAnswer]
       ON [dbo].[CandidateTestQuestionOptions]
AFTER UPDATE
AS
BEGIN
	BEGIN TRANSACTION

       SET NOCOUNT ON;
	   	DECLARE @QuestionId	UNIQUEIDENTIFIER,
		@CandidateTestDetailId UNIQUEIDENTIFIER

		SELECT @QuestionId=FkQuestionId FROM INSERTED i;
		SELECT @CandidateTestDetailId=FkCandidateTestDetailId FROM CandidateTestQuestions WHERE CandidateTestQuestionId=@QuestionId
		

		 UPDATE TD 
			SET TD.IsPartiallyCorrectAnswered=CASE WHEN TCA.CountIsCorrect <> TICA.CountIsCorrectAnswer AND TICA.CountIsCorrectAnswer > 0 THEN 1
                                        ELSE 0
									END,
			TD.IsFullyCorrectAnswered=CASE WHEN TCA.CountIsCorrect = TICA.CountIsCorrectAnswer THEN 1
                                        ELSE 0
									END,
			TD.IsQuestionAnswered=1,
			TD.TotalCandidateScoreObtained=ISNULL(TICA.TotalScore,0)										
		FROM CandidateTestQuestions TD
		LEFT JOIN (SELECT COUNT(IsCorrect) AS CountIsCorrect, FkQuestionId  FROM CandidateTestQuestionOptions
		 WHERE IsDeleted=0 AND FkQuestionId=@QuestionId AND IsCorrect = 1
		 GROUP BY FkQuestionId) TCA  ON TCA.FkQuestionId=TD.CandidateTestQuestionId
		 LEFT JOIN (SELECT COUNT(IsCorrectAnswer) AS CountIsCorrectAnswer,SUM(QuestionAnswerScore) TotalScore, FkQuestionId  FROM CandidateTestQuestionOptions
		 WHERE IsDeleted=0 AND FkQuestionId=@QuestionId AND IsCorrectAnswer = 1
		 GROUP BY FkQuestionId) TICA ON TICA.FkQuestionId=TD.CandidateTestQuestionId


		 UPDATE TD 
			SET TD.TotalCorrectAnswers=ISNULL(TCA.CountFullyCorrectAnswered,0)
				,TD.TotalWrongAnswers=ISNULL((ISNULL(TQA.CountQuestionAnswered,0)-ISNULL(TCA.CountFullyCorrectAnswered,0)-ISNULL(TPA.CountPartiallyCorrectAnswered,0)),0)
				,TD.TotalPartialAnswers=ISNULL(TPA.CountPartiallyCorrectAnswered,0)
				,TD.TotalAnsweredQuestions=ISNULL(TQA.CountQuestionAnswered,0)
				,TD.TotalCorrectPartialAnswers=ISNULL((ISNULL(TCA.CountFullyCorrectAnswered,0)+ISNULL(TPA.CountPartiallyCorrectAnswered,0)),0)
				,TD.IsCompleted=CASE WHEN TA.CountCandidateTestDetailId=TQA.CountQuestionAnswered THEN 1
									ELSE 0 END	
				,TD.TotalCandidateScoreObtained=CASE WHEN TSA.TotalScore<=TD.MaxScore THEN TSA.TotalScore
									ELSE TD.MaxScore END
		FROM CandidateTestDetails TD
		LEFT JOIN (SELECT COUNT(IsFullyCorrectAnswered) AS CountFullyCorrectAnswered, FkCandidateTestDetailId  FROM CandidateTestQuestions
		 WHERE IsDeleted=0 AND FkCandidateTestDetailId=@CandidateTestDetailId AND IsFullyCorrectAnswered = 1
		 GROUP BY FkCandidateTestDetailId) TCA  ON TCA.FkCandidateTestDetailId=TD.CandidateTestDetailId
		 LEFT JOIN (SELECT COUNT(IsPartiallyCorrectAnswered) AS CountPartiallyCorrectAnswered, FkCandidateTestDetailId  FROM CandidateTestQuestions
		 WHERE IsDeleted=0 AND FkCandidateTestDetailId=@CandidateTestDetailId AND IsPartiallyCorrectAnswered = 1
		 GROUP BY FkCandidateTestDetailId) TPA  ON TPA.FkCandidateTestDetailId=TD.CandidateTestDetailId
		 LEFT JOIN (SELECT COUNT(IsQuestionAnswered) AS CountQuestionAnswered, FkCandidateTestDetailId  FROM CandidateTestQuestions
		 WHERE IsDeleted=0 AND FkCandidateTestDetailId=@CandidateTestDetailId AND IsQuestionAnswered = 1
		 GROUP BY FkCandidateTestDetailId) TQA  ON TQA.FkCandidateTestDetailId=TD.CandidateTestDetailId
		 LEFT JOIN (SELECT COUNT(FkCandidateTestDetailId) AS CountCandidateTestDetailId, FkCandidateTestDetailId  FROM CandidateTestQuestions
		 WHERE IsDeleted=0 AND FkCandidateTestDetailId=@CandidateTestDetailId
		 GROUP BY FkCandidateTestDetailId) TA  ON TA.FkCandidateTestDetailId=TD.CandidateTestDetailId
		  LEFT JOIN (SELECT SUM(TotalCandidateScoreObtained) TotalScore, FkCandidateTestDetailId  FROM CandidateTestQuestions
		 WHERE IsDeleted=0 AND FkCandidateTestDetailId=@CandidateTestDetailId
		 GROUP BY FkCandidateTestDetailId) TSA  ON TSA.FkCandidateTestDetailId=TD.CandidateTestDetailId

	COMMIT TRANSACTION;

END
