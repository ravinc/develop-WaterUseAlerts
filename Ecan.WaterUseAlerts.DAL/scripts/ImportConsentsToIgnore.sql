/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [Hilltop].[dbo].[DailyAlertConsentsToFilter]
  where ignorereasonid is null

  alter table [dbo].[DailyAlertConsentsToFilter] add ReasonID int

  update cti set cti.ReasonID = dair.ID 
	from [dbo].[DailyAlertConsentsToFilter] cti
	inner join [dbo].[DailyAlertIgnoreReason] dair on dair.[Description] = cti.[IgnoreReasonID] 

insert into [dbo].[DailyAlertIgnoreList]
([ConsentID or Water Group], [IgnoreReasonID], [Comment], [CreatedBy], [StartDate], [EndDate])
(select [ConsentID_or_Water_Group], [ReasonID], [Comment], [CreatedBy], [StartDate], [EndDate] from [dbo].[DailyAlertConsentsToFilter])

update da set da.[Ignore]=1
	from [dbo].[DailyAlert] da
	inner join [dbo].[DailyAlertIgnoreList] dail on dail.[ConsentID or Water Group]=da.[ConsentID or Water Group]