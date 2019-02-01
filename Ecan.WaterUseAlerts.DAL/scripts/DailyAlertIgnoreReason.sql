USE [Hilltop]
GO

/****** Object:  Table [dbo].[DailyAlertIgnoreReason]    Script Date: 15/01/2019 2:46:09 PM ******/
DROP TABLE [dbo].[DailyAlertIgnoreReason]
GO

/****** Object:  Table [dbo].[DailyAlertIgnoreReason]    Script Date: 15/01/2019 2:46:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyAlertIgnoreReason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NOT NULL,
 CONSTRAINT [PK_DailyAlertIgnoreReason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--  delete from [dbo].[DailyAlertIgnoreReason]

insert into [DailyAlertIgnoreReason] (Description) values('Issue with Meter')
insert into [DailyAlertIgnoreReason] (Description) values('Consents with Associated WAPS')
insert into [DailyAlertIgnoreReason] (Description) values('Under 5l/s')
insert into [DailyAlertIgnoreReason] (Description) values('Associated Permits Confirmed')
insert into [DailyAlertIgnoreReason] (Description) values('Complicated Allocation')
insert into [DailyAlertIgnoreReason] (Description) values('Regionally Significant')
insert into [DailyAlertIgnoreReason] (Description) values('Associated Permits Assumed')
insert into [DailyAlertIgnoreReason] (Description) values('Other')


