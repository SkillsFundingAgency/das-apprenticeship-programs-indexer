CREATE PROCEDURE [dbo].[GetAchievementRatesProvider]
AS

SELECT 
	[UKPRN], 
	[Age],
	[Apprenticeship Level] as ApprenticeshipLevel,
	[Overall Cohort] as OverallCohort, 
	[Overall Achivement Rate %] as OverallAchievementRate,
	[Sector Subject Area Tier 2] as SectorSubjectAreaTier2,
	[SSA2 Code] as SSA2Code
	FROM [dbo].[AchievementRatesProvider]
	WHERE [Age] = 'All Age'
	AND [Sector Subject Area Tier 2] <> 'All SSA T2'
	AND [Apprenticeship Level] <> 'All'
	AND [HybridYear] = (SELECT MAX([HybridYear]) FROM [dbo].[AchievementRatesProvider])