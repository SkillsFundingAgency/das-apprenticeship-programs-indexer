/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

UPDATE [dbo].[EmployerSatisfaction] SET HybridYear = CONVERT(varchar(4), (Year-1)) + '/' + CONVERT(varchar(4), (Year))
WHERE HybridYear = ''

UPDATE [dbo].[LearnerSatisfaction] SET HybridYear = CONVERT(varchar(4), (Year-1)) + '/' + CONVERT(varchar(4), (Year))
WHERE HybridYear = ''