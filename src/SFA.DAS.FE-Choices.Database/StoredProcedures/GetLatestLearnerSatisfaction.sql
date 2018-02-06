CREATE PROCEDURE [dbo].[GetLatestLearnerSatisfaction]
AS
SELECT  [UKPRN], FinalScore, [Learners] AS TotalCount,[Responses] AS ResponseCount 
	FROM [dbo].[LearnerSatisfaction]
	WHERE [HybridYear] = (SELECT MAX([HybridYear]) FROM [dbo].[LearnerSatisfaction])