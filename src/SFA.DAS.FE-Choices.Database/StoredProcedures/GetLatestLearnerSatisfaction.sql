CREATE PROCEDURE [dbo].[GetLatestLearnerSatisfaction]
AS
SELECT  [UKPRN], FinalScore, [Learners] AS TotalCount,[Responses] AS ResponseCount 
	FROM [dbo].[LearnerSatisfaction]
	WHERE [Year] = (SELECT MAX([Year]) FROM [dbo].[LearnerSatisfaction])