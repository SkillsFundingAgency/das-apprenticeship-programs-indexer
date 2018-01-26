CREATE TABLE [dbo].[LearnerSatisfaction]
(
	[UKPRN] BIGINT NOT NULL,
	[ProviderName] NVARCHAR(100) NOT NULL,
	[OrganisationType] NVARCHAR(100) NOT NULL,
	[FinalScore] FLOAT NULL,
	[MSRCDescription] NVARCHAR(100) NULL,
	[Learners] INT NOT NULL default(0),
	[Responses] INT NOT NULL default(0),
	[Year] NVARCHAR(10) NOT NULL
)