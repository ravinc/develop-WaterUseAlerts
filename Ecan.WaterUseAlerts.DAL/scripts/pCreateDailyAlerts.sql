USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pCreateDailyAlerts]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pCreateDailyAlerts]
GO

/****** Object:  StoredProcedure [dbo].[pCreateDailyAlerts]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 01/02/2019
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pCreateDailyAlerts](
	@AlertID int out
	,@ServiceProvider varchar(200)
	,@ConsentID varchar(200)
	,@ActivityNumber varchar(200)
	,@Meters varchar(1024)
	,@Type varchar(200)
	,@Limit varchar(200)
	,@MaxUse varchar(200)
	,@PercentOverLimit varchar(200)
	,@DateofHighestOverage datetime
	,@DateofEarliestNoncompliance datetime
	,@DateofLatestNoncompliance datetime
	,@Ignore int
	,@Rundate datetime
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

insert into [dbo].[DailyAlert]
	([Service Provider]
	,[ConsentID or Water Group]
	,[Activity Number]
	,[Meters]
	,[Type]
	,[Limit]
	,[Max Use]
	,[Percent Over Limit]
	,[Date of Highest Overage]
	,[Date of Earliest Non-compliance]
	,[Date of Latest Non-compliance]
	,[RunDate]
	,[StatusID]
	,[Ignore]
	,[LastModifiedDate])
	values(
	 @ServiceProvider
	,@ConsentID
	,@ActivityNumber
	,@Meters
	,@Type
	,@Limit
	,@MaxUse
	,@PercentOverLimit
	,@DateofHighestOverage
	,@DateofEarliestNoncompliance
	,@DateofLatestNoncompliance
	,@Rundate
	,4
	,@Ignore
	,getdate())

	SET @AlertID = SCOPE_IDENTITY();

END
GO

Grant execute on [dbo].[pCreateDailyAlerts] to public
GO


