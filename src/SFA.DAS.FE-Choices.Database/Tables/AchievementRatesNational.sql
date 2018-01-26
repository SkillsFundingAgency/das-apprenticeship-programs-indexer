CREATE TABLE [dbo].[AchievementRatesNational]
(
    [Age] NVARCHAR(255),
	[Sector Subject Area Tier 1] NVARCHAR(255),
	[Sector Subject Area Tier 2] NVARCHAR(255),
	[Apprenticeship Level] NVARCHAR(255),
	[Apprenticeship Type] NVARCHAR(255),
	[Overall Cohort] FLOAT,
	[Overall Achievement Rate %] FLOAT NULL,
	[Institution Type] NVARCHAR(255),
	[SSA1] float,
	[SSA2] float,
	[HybridYear] VARCHAR(255) NULL
)