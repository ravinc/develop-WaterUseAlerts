USE [Hilltop]
GO

/****** Object:  Table [dbo].[DailyAlertStatus]    Script Date: 15/01/2019 2:46:09 PM ******/
DROP TABLE [dbo].[DailyAlertStatus]
GO

/****** Object:  Table [dbo].[DailyAlertStatus]    Script Date: 15/01/2019 2:46:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyAlertStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NOT NULL,
 CONSTRAINT [PK_DailyAlertStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into [DailyAlertStatus] (Description) values('Not Set')
insert into [DailyAlertStatus] (Description) values('Investigate (In process)')
insert into [DailyAlertStatus] (Description) values('Escalated')
insert into [DailyAlertStatus] (Description) values('Reject (false alert)')


