USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pSearchDailyAlerts]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pSearchDailyAlerts]
GO

/****** Object:  StoredProcedure [dbo].[pSearchDailyAlerts]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 21/20/2018
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pSearchDailyAlerts](
	@IncludeAll varchar(3) = 'No'
	,@StartDate datetime = null
	,@EndDate datetime = null
	,@Username varchar(64) = null
	,@ConsentID varchar(200) = null
	,@Type varchar(200) = null
	,@ActivityNumber varchar(200) = null
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT da.ID,
			da.[Service Provider]          as [ServiceProvider],
			da.[ConsentID or Water Group] as [ConsentID],
			da.[RunDate]				  as [RunDate],
			da.[Activity Number]          as [ActivityNumber],
			da.[Meters],
			da.[Type],
			da.[Limit],
			da.[Max Use]                  as [MaxUse],
			da.[Percent Over Limit]       as [PercentOverLimit],
			da.[Date of Highest Overage]  as [MaxAvDate],
			da.[Date of Earliest Non-compliance] as [FirstNCDate],
			da.[Date of Latest Non-compliance] as [LastNCDate],
			case isnull(StatusID,0)
				when 0 then 'Not Set'
				else (select [Description] from [dbo].[DailyAlertStatus] where [ID] = da.StatusID)
			end as [Status],
			da.[Ignore],
			case isnull(da.[Ignore],0)
				when 0 then null
				else (select dair.[Description] from [dbo].[DailyAlertIgnoreList] dail
						inner join [dbo].[DailyAlertIgnoreReason] dair on dail.[IgnoreReasonID]=dair.ID
						where dail.[ConsentID or Water Group] = da.[ConsentID or Water Group]
						and da.Rundate >= dail.[StartDate] and (dail.[EndDate] is null or da.Rundate <= dail.[EndDate]))
			end as [IgnoreReason],
			da.Comment,
			da.[LastModifiedDate],
			da.[Username]
	FROM [dbo].[DailyAlert] da
	WHERE ((@IncludeAll = 'No' and da.[Ignore] = 0) or @IncludeAll = 'Yes')
	and (@StartDate is null or da.RunDate >= @StartDate)
	and (@EndDate is null or da.RunDate <= @EndDate)
	and (@Username is null or da.[Username] = @Username)
	and (@ConsentID is null or da.[ConsentID or Water Group] = @ConsentID)
	and (@Type is null or da.[Type]= @Type)
	and (@ActivityNumber is null or da.[Activity Number] = @ActivityNumber)
END
GO

Grant execute on [dbo].[pSearchDailyAlerts] to public
GO


