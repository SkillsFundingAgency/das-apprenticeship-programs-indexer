# Import achievement rate data

First read this [guide](https://skillsfundingagency.atlassian.net/wiki/spaces/DAS/pages/287113242/Achievement+Rates).

Following the guide didn’t completely work with the 2017/18 data, although the guide still remains extremely useful.

Here's my notes from the 2017-2018 import process.
 
Importing the flat file into the database hit issues. SSMS showed preview data ok, but would not import the file correctly (it was insisting on trying to convert columns with numbers in, to numeric types, even after specifying the column as nvarchar, then refused to import the file as some rows didn’t contain numbers!)

Instead, here’s how i imported the raw data

* Copy schema of the 2 tables, changing floats to nvarchar, see

   CreateImportTableForAchievementRatesNational.sql  
   CreateImportTableForAchievementRatesProvider.sql
    
* Import csv files into Excel, see

    NART205_201718_NationalAchievementRateTables_Apprenticeship_Overall_SectorSubjectArea.csv  
    NART217_201718_NationalAchievementRateTables_Apprenticeship_Overall_Institution_SectorSubjectArea.csv

* Script insert into's. Add (and fill) into excel column, e.g. 

`    INSERT INTO [dbo].[NationalRaw] ([Age],[Sector Subject Area Tier 1],[Sector Subject Area Tier 2],[Apprenticeship Level],[Apprenticeship Type],[Overall Cohort],[Overall Achievement Rate %],[Institution Type],[SSA1],[SSA2],[HybridYear]) VALUES(`

* Add (and fill formula) into excel column, i.e.

`    ="'"&C2&"','"&D2&"','"&E2&"','"&F2&"','"&G2&"','"&H2&"','a"&I2&"','"&A2&"','','','"&B2&"')"
    ="'"&B2&"','"&E2&"','"&F2&"','"&G2&"','"&H2&"','"&I2&"','"&C2&"','"&A2&"','"&J2&"','"&K2&"','','','"&E2&"')"
`
* Add (and fill) concat column to join insert column & values column

* Do some manual data cleansing, e.g. some strings have single quotes.

* Copy and paste generated insert statements out of Excel and run in SSMS, into empty deployed database

* Copy from raw imports into destination table, manipulating some fields as per earlier guide, these stored procs [1](https://github.com/SkillsFundingAgency/das-apprenticeship-programs-indexer/blob/master/src/SFA.DAS.FE-Choices.Database/StoredProcedures/GetAchievementRatesNational.sql) , [2](https://github.com/SkillsFundingAgency/das-apprenticeship-programs-indexer/blob/master/src/SFA.DAS.FE-Choices.Database/StoredProcedures/GetAchievementRatesProvider.sql) , and the [code](https://github.com/SkillsFundingAgency/das-apprenticeship-programs-indexer/blob/master/src/Sfa.Das.Sas.Indexer.ApplicationServices/Provider/Services/ProviderIndexer.cs) . See...
    
    InsertNewAchievementRatesNational.sql
    
    InsertNewAchievementRatesProvider.sql

* Run these scripts to populate the SSA Code columns

`    UpdateNationalSsaCodes.sql  
    UpdateProviderSsaCodes.sql
`
* Generate insert statements from national/provider tables (use Tasks -> Generate scripts...)

* Get devops to run insert scripts in test environment & testers to test. if ok, get devops to run insert scripts in pp/prod (etc?)

# Notes

Appendix 2 of this pdf contains the SSA codes...
https://assets.publishing.service.gov.uk/government/uploads/system/uploads/attachment_data/file/732683/Learning_Aim_Class_Codes_2018_to_2019.pdf

# Whats left to do

Check generated data in app

preprod provider:
	  select distinct HybridYear
  FROM [dbo].[AchievementRatesProvider]
2015/16
2016/17
2014/15

new data provider:
19-23
All Age
24+
16-18

how to reconcile?
