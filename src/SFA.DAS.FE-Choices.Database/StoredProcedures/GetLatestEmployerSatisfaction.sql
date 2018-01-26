CREATE PROCEDURE [dbo].[GetLatestEmployerSatisfaction]
AS

SELECT  [UKPRN] FinalScore, [Employers] AS TotalCount, [Responses] AS ResponseCount
FROM [dbo].[EmployerSatisfaction]
WHERE [Year] = (SELECT MAX([Year]) FROM [dbo].[EmployerSatisfaction])