INSERT INTO [dbo].[AchievementRatesNational]
([Age]
,[Sector Subject Area Tier 1]
,[Sector Subject Area Tier 2]
,[Apprenticeship Level]
,[Apprenticeship Type]
,[Overall Cohort]
,[Overall Achievement Rate %]
,[Institution Type]
,[SSA1]
,[SSA2]
,[HybridYear])
select
    Age,
    [Sector Subject Area Tier 1],
    CASE [Sector Subject Area Tier 2] WHEN 'All Sector Subject Area  Tier 2' THEN 'All SSA T2' ELSE [Sector Subject Area Tier 2] END ,
    CASE [Apprenticeship Level] When 'All Levels' THEN 'All' else [Apprenticeship Level] end,
    [Apprenticeship Type],
    case [Overall Cohort] when '-' then null else convert(float,replace([Overall Cohort],',','')) end,
    case [Overall Achievement Rate %] when '-' then null else convert(float,[Overall Achievement Rate %]) end,
    [Institution Type],
    null, --todo? no source SSA1 convert(float,[SSA1]),
    null, --todo? no source SSA2 case [ssa2] when 'NULL' then null else convert(float,[SSA2]) end,
    [HybridYear]
from [dbo].[NationalRaw]