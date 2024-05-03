IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<<>>
-- Create date: 22 Feb 2018
-- Description:	Used to split data seperated with a character
-- =============================================
CREATE FUNCTION [dbo].[split] 
(	
  @RowData nvarchar(max)
 ,@SplitOn nvarchar(5)  
)
RETURNS @RtnValue table   
(  
 Id int identity(1,1),  
 Data nvarchar(max)  
) 
AS
BEGIN
     Declare @Cnt int  
     Set @Cnt = 1  
  
     While (Charindex(@SplitOn,@RowData)>0)  
     Begin  
           Insert Into @RtnValue (data)  
           Select   
                  Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))  
  
           Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))  
           Set @Cnt = @Cnt + 1  
     End  
   
 Insert Into @RtnValue (data)  
 Select ltrim(rtrim(@RowData))  
  
 Return 

END

' 
END

GO