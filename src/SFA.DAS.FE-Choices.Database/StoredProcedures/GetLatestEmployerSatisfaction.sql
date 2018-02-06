CREATE PROCEDURE [dbo].[GetLatestEmployerSatisfaction]
AS

SELECT  [UKPRN] FinalScore, [Employers] AS TotalCount, [Responses] AS ResponseCount
FROM [dbo].[EmployerSatisfaction]
WHERE [HybridYear] = (SELECT MAX([HybridYear]) FROM [dbo].[EmployerSatisfaction])