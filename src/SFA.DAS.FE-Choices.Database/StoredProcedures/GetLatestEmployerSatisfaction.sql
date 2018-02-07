CREATE PROCEDURE [dbo].[GetLatestEmployerSatisfaction]
AS

SET NOCOUNT ON;
Declare @hybridYear varchar(10)

set @hybridYear  = (SELECT MAX([HybridYear]) FROM [dbo].[EmployerSatisfaction])

SELECT  [UKPRN] FinalScore, [Employers] AS TotalCount, [Responses] AS ResponseCount
FROM [dbo].[EmployerSatisfaction]
WHERE [HybridYear] = @hybridYear