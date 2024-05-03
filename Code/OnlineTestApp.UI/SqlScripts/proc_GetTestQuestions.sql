IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTestQuestions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetTestQuestions] AS' 
END
GO
ALTER PROCEDURE [dbo].[GetTestQuestions] 
	@questionTechnology		UNIQUEIDENTIFIER	=	'B1356190-AC8C-49DD-AEAE-9BDBE0B70F19'
	,@questionLevel			UNIQUEIDENTIFIER	=	'B1356190-AC8C-49DD-AEAE-9BDBE0B70F19'
	,@questionTime			INT					=	30
	,@count					INT					=	1
	--,@questionScore			INT					=	50
AS
BEGIN
	
	SET NOCOUNT ON;
	
		DECLARE @diffTime INT

	SELECT * into #Temp from (
			 SELECT ROW_NUMBER() OVER(Partition by Q.QuestionId ORDER BY NEWID()) AS rn,
			   Q.*, QT.FkQuestionTechnology, QL.FkQuestionLevel
			   ,s = SUM(Q.TotalTime) OVER 
			   (
				 ORDER BY NEWID()
			   )				
			 FROM Questions Q
			 INNER JOIN QuestionLevels QL ON Q.QuestionId=QL.FkQuestionId and Q.IsDeleted=0
			 INNER JOIN QuestionTechnologies QT ON Q.QuestionId=QT.FkQuestionId 
			 INNER JOIN [dbo].Split(@questionTechnology,',') as S ON S.Data = QT.FkQuestionTechnology AND S.Data <> ''
			 WHERE QL.FkQuestionLevel = @questionLevel
			 ) t where rn=1
		
		SELECT * into #t
			FROM #Temp 
			WHERE s <= @questionTime
			ORDER BY s
			
		select @diffTime=@questionTime-max(s) from #t	

	
		;WITH CTE
		as(
			SELECT 
				   * 
				   ,r = SUM(TotalTime) OVER 
				   (
					 ORDER BY NEWID()
				   )		    
				 from #Temp 
				 where QuestionId not in (select QuestionId from #t)
		
		)		
		
				select  * into #TT from CTE 
		where  r <= @diffTime
			ORDER BY r
			select * INTO #tempFinal from 

			(
		select * from #tt
		union 
		select *,1 from #t) tableFinal

			declare @QuestionIds VARCHAR(MAX)
		
		    SELECT @QuestionIds = COALESCE(@QuestionIds + ', ', '') + CAST(QuestionId AS VARCHAR(40)) FROM #tempFinal			
			declare @TempTestId uniqueidentifier = NewId()
			     
				 INSERT 
			     INTO TempTests
				          (
				            TempTestId
						   ,QuestionIds
						   ,TotalTime
						   ,TotalQuestions
						   ,CreatedDateTime
						   ,[FkCreatedBy]
						  )

			     select     @TempTestId
				           ,@QuestionIds as QuestionIds	
						   ,SUM(TotalTime) as TotalTime
						   ,count(*) as TotalQuestions
						   ,GETDATE()						   
						   ,@loggedInUserId
						    from #tempFinal 

            --output
			SELECT 			TempTestId
						   ,QuestionIds
						   ,TotalTime
						   ,TotalQuestions
						    FROM TempTests WHERE TempTestId = @TempTestId
					

			drop table #t
			drop table #tt
			drop table #Temp
			drop table #tempFinal
	
	
END
GO
