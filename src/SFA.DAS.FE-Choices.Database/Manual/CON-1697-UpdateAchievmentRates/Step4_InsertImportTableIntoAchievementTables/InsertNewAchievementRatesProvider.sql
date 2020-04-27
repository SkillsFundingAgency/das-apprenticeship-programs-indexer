INSERT INTO [dbo].[AchievementRatesProvider]
([UKPRN]
,[Age]
,[Sector Subject Area Tier 1]
,[Sector Subject Area Tier 2]
,[Apprenticeship Level]
,[Apprenticeship Type]
,[Institution Type]
,[Institution Name]
,[Overall Cohort]
,[Overall Achivement Rate %]
,[SSA1 Code]
,[SSA2 Code]
,[HybridYear])

SELECT [UKPRN]
     ,[Age]
     ,[Sector Subject Area Tier 1]
     ,CASE [Sector Subject Area Tier 2] WHEN 'All Sector Subject Area  Tier 2' THEN 'All SSA T2' ELSE [Sector Subject Area Tier 2] END
     ,CASE [Apprenticeship Level] WHEN 'All Levels' THEN 'All' else [Apprenticeship Level] end
     ,[Apprenticeship Type]
     ,[Institution Type]
     ,[Institution Name]
    -- preprod has - & *, so don't convert them to null
    --,case when [overall cohort] in ('-', '*') then null else replace([Overall Cohort],',','') end
     ,replace([Overall Cohort],',','')
     -- guide says to exclude rows where overall achievement rate is not present, but preprod contains rows with it set to null, so bring them all in
     ,case when [Overall Achivement Rate %] in ('-', '*') then null
           else convert(float,replace( [Overall Achivement Rate %],',','')) end

     ,null
     ,null
     ,[hybridyear]
FROM [dbo].[ProviderRaw]