USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pDailyAlertStats]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pDailyAlertStats]
GO

/****** Object:  StoredProcedure [dbo].[pDailyAlertStats]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 21/20/2018
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pDailyAlertStats](
	@StartDate datetime
	,@EndDate datetime
)
AS
BEGIN
	SET NOCOUNT ON;

	-- declare @StartDate datetime = '21-Jan-2019'
	-- declare @EndDate datetime = '27-Jan-2019'

	create table #StatsCount (StatsType varchar(50), [Count] int)

	insert into #StatsCount (StatsType, [Count])(
		SELECT 'Total Alerts', count(distinct [ConsentID or Water Group])
		FROM [dbo].[DailyAlert]
		where [RunDate] >= @StartDate and Rundate <= @EndDate)
	
	insert into #StatsCount (StatsType, [Count])(
		SELECT 'Total Ignored', count(distinct [ConsentID or Water Group])
		FROM [dbo].[DailyAlert]
		where [RunDate] >= @StartDate and Rundate <= @EndDate
		and Ignore = 1)
	
	insert into #StatsCount (StatsType, [Count])(
		SELECT 'Total To Be Processed', count(distinct [ConsentID or Water Group])
		FROM [dbo].[DailyAlert]
		where [RunDate] >= @StartDate and Rundate <= @EndDate
		and Ignore = 0)
	
	create table #Stats (ConsentID varchar(200), [Status] varchar(250))
	
	insert into #Stats (ConsentID)
		SELECT distinct [ConsentID or Water Group]
			FROM [dbo].[DailyAlert]
			where [RunDate] >= @StartDate and Rundate <= @EndDate
			and [Ignore] = 0

	update #Stats set
		[Status] = (select [Description] from [dbo].[DailyAlertStatus] das
						where das.ID = (
							select min(StatusID) from [dbo].[DailyAlert] da
								where da.RunDate >= @StartDate and da.Rundate <= @EndDate
								and da.[ConsentID or Water Group] = #Stats.ConsentID)
								)

	insert into #StatsCount (StatsType, [Count])(
		SELECT '  -  ' + [Status], count(*)
		FROM #Stats
		group by [Status])

	select * from #StatsCount

	drop table #Stats
	drop table #StatsCount

END
GO

Grant execute on [dbo].[pDailyAlertStats] to public
GO


