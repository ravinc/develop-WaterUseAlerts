USE [Hilltop]
GO

insert into [dbo].[DailyAlert](
				[Service Provider]
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
				,[LastModifiedDate]
				,[Processed]
)	(select 
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
				,[RunDate]
				,4
				,0
				,getdate()
				,0
 from [dbo].[DailyAlertsCombined])

 -- delete  from [dbo].[DailyAlert]


