USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pGetIgnoreList]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pGetIgnoreList]
GO

/****** Object:  StoredProcedure [dbo].[pGetIgnoreList]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 01/02/2019
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pGetIgnoreList](
	@StartDate datetime = null
	,@EndDate datetime = null
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT dail.ID
	  ,[ConsentID or Water Group] as ConsentID
      ,dair.[Description] as Reason
      ,[Comment]
      ,[CreatedBy]
      ,[StartDate]
      ,[EndDate]
  FROM [dbo].[DailyAlertIgnoreList] dail
  inner join [dbo].[DailyAlertIgnoreReason] dair on dair.[ID] = dail.IgnoreReasonID
  where (@StartDate is null or dail.[StartDate] >= @StartDate)
  and (@EndDate is null or dail.[EndDate] <= @EndDate)
END
GO

Grant execute on [dbo].[pGetIgnoreList] to public
GO


