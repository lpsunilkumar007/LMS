IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_Questions_InsertUpdateScore]'))
BEGIN
DROP Trigger [dbo].[trg_Questions_InsertUpdateScore] 
END
GO

CREATE TRIGGER [dbo].[trg_Questions_InsertUpdateScore]
       ON [dbo].[QuestionOptions]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
       SET NOCOUNT ON;
	   	DECLARE @QuestionsId	UNIQUEIDENTIFIER

		 IF EXISTS (Select * from INSERTED)
			BEGIN
				SELECT @QuestionsId=FkQuestionId FROM INSERTED i;
			END
		ELSE IF EXISTS(Select * from DELETED)
			BEGIN
				SELECT @QuestionsId=FkQuestionId FROM DELETED i;
			END

			
       UPDATE Q 
			SET Q.TotalScore=T.TotalSum,
			Q.TotalOptions=T.TotalCount
		FROM Questions Q
		INNER JOIN (SELECT COUNT(FkQuestionId) AS TotalCount, SUM(QuestionAnswerScore) AS TotalSum, FkQuestionId  FROM QuestionOptions WHERE IsDeleted=0 AND FkQuestionId=@QuestionsId
		 GROUP BY FkQuestionId) T 
		 ON T.FkQuestionId=Q.QuestionId
END
GO