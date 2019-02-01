use [Hilltop]

delete from [dbo].[DailyAlert]


insert into [dbo].[DailyAlert]
	([Service Provider]
	,[ConsentID or Water Group]
	,[Activity Number]
	,[Meters]
	,[Type]
	,[Limit]
	,[Max Use]
	,[Percent Over Limit]
	,[Date of Highest Overage]
	,[Date of Earliest Non-compliance]
	,[Date of Latest Non-compliance]
	,[RunDate]
	,[StatusID]
	,[Ignore]
	,[LastModifiedDate])
	(select
	 [Service_Provider]
	,[ConsentID_or_Water_Group]
	,[Activity_Number]
	,[Meters]
	,[Type]
	,[Limit]
	,[Max_Use]
	,[Percent_Over_Limit]
	,[Date_of_Highest_Overage]
	,[Date_of_Earliest_Non_compliance]
	,[Date_of_Latest_Non_compliance]
	,[Rundate]
	,4
	,0
	,getdate()
	from [dbo].[DailyAlertCombined])

update [dbo].[DailyAlert] set [Type]='Low Flow Ban' where [Type]='Take Rate - Low Flow Ban'

update da set da.[Ignore]=1
	from [dbo].[DailyAlert] da
	inner join [dbo].[DailyAlertIgnoreList] dail on dail.[ConsentID or Water Group]=da.[ConsentID or Water Group]
