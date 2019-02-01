USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pUpdateDailyAlerts]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pUpdateDailyAlerts]
GO

/****** Object:  StoredProcedure [dbo].[pUpdateDailyAlerts]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 18/01/2019
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pUpdateDailyAlerts](
	@ID int
	,@Ignore int
	,@Status varchar(100)
	,@Comment varchar(200)
	,@Username varchar(64)
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update da set
		da.[Ignore] = @Ignore
		,da.[StatusID] = das.ID
		,da.[Comment] = @Comment
		,da.[Username] = @Username
		,da.LastModifiedDate = getdate()
	from [dbo].[DailyAlert] da
	inner join [dbo].[DailyAlertStatus] das on das.[Description] = @Status
	where da.ID = @ID

END
GO

Grant execute on [dbo].[pUpdateDailyAlerts] to public
GO


