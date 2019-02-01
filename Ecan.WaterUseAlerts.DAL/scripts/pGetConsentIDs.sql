USE [hilltop]
GO

/****** Object:  StoredProcedure [dbo].[pGetConsentIDs]    Script Date: 21/12/2018 10:51:22 AM ******/
DROP PROCEDURE [dbo].[pGetConsentIDs]
GO

/****** Object:  StoredProcedure [dbo].[pGetConsentIDs]    Script Date: 21/12/2018 10:51:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		R Chand
-- Create date: 21/20/2018
-- Description:	Daily Alerts search.
-- =============================================
CREATE PROCEDURE [dbo].[pGetConsentIDs]

AS
BEGIN
	SET NOCOUNT ON;

    SELECT distinct [ConsentID or Water Group] as [ConsentID]
	FROM [dbo].[DailyAlert]
END
GO

Grant execute on [dbo].[pGetConsentIDs] to public
GO


