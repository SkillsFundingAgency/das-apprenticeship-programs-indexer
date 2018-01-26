CREATE TABLE [dbo].[AchievementRatesProvider]
(
	[UKPRN] nvarchar(255),
	[Age] NVARCHAR(255),
	[Sector Subject Area Tier 1] NVARCHAR(255),
	[Sector Subject Area Tier 2] NVARCHAR(255),
	[Apprenticeship Level] NVARCHAR(255),
	[Apprenticeship Type] NVARCHAR(255),
	[Institution Type] NVARCHAR(255),
	[Institution Name] NVARCHAR(255),
	[Overall Cohort] NVARCHAR(255),
	[Overall Achivement Rate %] float NULL,
	[SSA1 Code] float,
	[SSA2 Code] float,
	[HybridYear] NVARCHAR(255)
)