USE [Hilltop]
GO

/****** Object:  Table [dbo].[DailyAlert]    Script Date: 24/01/2019 8:41:30 AM ******/
DROP TABLE [dbo].[DailyAlert]
GO

/****** Object:  Table [dbo].[DailyAlert]    Script Date: 24/01/2019 8:41:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyAlert](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Service Provider] [varchar](200) NULL,
	[ConsentID or Water Group] [varchar](200) NULL,
	[Activity Number] [varchar](200) NULL,
	[Meters] [varchar](1024) NULL,
	[Type] [varchar](200) NULL,
	[Limit] [varchar](200) NULL,
	[Max Use] [varchar](200) NULL,
	[Percent Over Limit] [varchar](200) NULL,
	[Date of Highest Overage] [date] NULL,
	[Date of Earliest Non-compliance] [date] NULL,
	[Date of Latest Non-compliance] [date] NULL,
	[RunDate] [datetime] NOT NULL,
	[StatusID] [int] NULL,
	[Ignore] [int] NOT NULL,
	[Comment] [varchar](200) NULL,
	[Username] [varchar](64) NULL,
	[LastModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DailyAlert] ADD  DEFAULT (CONVERT([date],getdate())) FOR [RunDate]
GO

ALTER TABLE [dbo].[DailyAlert] ADD  DEFAULT ((0)) FOR [Ignore]
GO

ALTER TABLE [dbo].[DailyAlert] ADD  DEFAULT (getdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[DailyAlert]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[DailyAlertStatus] ([ID])
GO

/****** Object:  Index [IDX_UNQ_ConsentRunDate]    Script Date: 24/01/2019 1:05:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IDX_UNQ_ConsentRunDateType] ON [dbo].[DailyAlert]
(
	[ConsentID or Water Group] ASC,
	[Activity Number] ASC,
	[RunDate] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



