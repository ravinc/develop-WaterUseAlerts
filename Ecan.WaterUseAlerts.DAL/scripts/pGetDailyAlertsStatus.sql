USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pGetDailyAlertsStatus]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pGetDailyAlertsStatus]
GO

/****** Object:  StoredProcedure [dbo].[pGetDailyAlertsStatus]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 21/01/2019
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pGetDailyAlertsStatus]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ID, [Description] from [dbo].[DailyAlertStatus]
	--	where [Description] not in ('Not Set')
END
GO

Grant execute on [dbo].[pGetDailyAlertsStatus] to public
GO


