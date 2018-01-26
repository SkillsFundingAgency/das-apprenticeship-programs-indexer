CREATE PROCEDURE [dbo].[GetAchievementRatesNational]
AS
	
SELECT 
	[Institution Type] as InstitutionType,
	[HybridYear] as HybridEndYear,
	[Age],
	[Sector Subject Area Tier 1] as SectorSubjectAreaTier1,
	[Sector Subject Area Tier 2] as SectorSubjectAreaTier2,
	[Apprenticeship Level] as ApprenticeshipLevel,
	[Overall Achievement Rate %] as OverallAchievementRate,
	[SSA2] as SSA2Code
	FROM [dbo].[AchievementRatesNational]
	WHERE [Institution Type] = 'All Institution Type'
	AND [Age] = 'All Age'
	AND [Sector Subject Area Tier 2] <> 'All SSA T2'
	AND [Apprenticeship Level] <> 'All'
	AND [HybridYear] = (SELECT MAX([HybridYear]) FROM [dbo].[AchievementRatesNational])