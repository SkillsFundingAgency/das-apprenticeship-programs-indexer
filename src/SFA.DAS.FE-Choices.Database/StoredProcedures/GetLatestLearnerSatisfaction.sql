CREATE PROCEDURE [dbo].[GetLatestLearnerSatisfaction]
AS

SET NOCOUNT ON;
Declare @hybridYear varchar(10)

set @hybridYear  = (SELECT MAX([HybridYear]) FROM [dbo].[LearnerSatisfaction])

SELECT  [UKPRN], FinalScore, [Learners] AS TotalCount,[Responses] AS ResponseCount 
	FROM [dbo].[LearnerSatisfaction]
	WHERE [HybridYear] = @hybridYear 