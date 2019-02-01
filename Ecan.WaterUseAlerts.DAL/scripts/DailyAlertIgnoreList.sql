USE [Hilltop]
GO

/****** Object:  Table [dbo].[DailyAlertIgnoreList]    Script Date: 15/01/2019 3:29:37 PM ******/
DROP TABLE [dbo].[DailyAlertIgnoreList]
GO

/****** Object:  Table [dbo].[DailyAlertIgnoreList]    Script Date: 15/01/2019 3:29:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyAlertIgnoreList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConsentID or Water Group] [varchar](200) NOT NULL,
	[IgnoreReasonID] [int] NOT NULL,
	[Comment] [varchar](200) NULL,
	CreatedBy varchar(60),
	StartDate datetime not null,
	EndDate datetime
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DailyAlertIgnoreList] ADD  DEFAULT (getdate()) FOR StartDate
GO

ALTER TABLE [dbo].[DailyAlertIgnoreList]  WITH CHECK ADD FOREIGN KEY([IgnoreReasonID])
REFERENCES [dbo].DailyAlertIgnoreReason ([ID])
GO

CREATE UNIQUE INDEX IDX_UNQ_ConsentStartDate ON [dbo].[DailyAlertIgnoreList]([ConsentID or Water Group], StartDate);
GO



