/****** Object:  Table [dbo].[ProviderRaw]    Script Date: 21/04/2020 11:46:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProviderRaw](
	[UKPRN] [nvarchar](255) NULL,
	[Age] [nvarchar](255) NULL,
	[Sector Subject Area Tier 1] [nvarchar](255) NULL,
	[Sector Subject Area Tier 2] [nvarchar](255) NULL,
	[Apprenticeship Level] [nvarchar](255) NULL,
	[Apprenticeship Type] [nvarchar](255) NULL,
	[Institution Type] [nvarchar](255) NULL,
	[Institution Name] [nvarchar](255) NULL,
	[Overall Cohort] [nvarchar](255) NULL,
	[Overall Achivement Rate %] [nvarchar](255) NULL,
	[SSA1 Code] [nvarchar](255) NULL,
	[SSA2 Code] [nvarchar](255) NULL,
	[HybridYear] [nvarchar](255) NULL
) ON [PRIMARY]
GO


