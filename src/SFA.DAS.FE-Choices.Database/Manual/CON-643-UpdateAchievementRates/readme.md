First read this guide: https://skillsfundingagency.atlassian.net/wiki/spaces/DAS/pages/287113242/Achievement+Rates

Following the guide didn’t work with the 2017/18 data, although the guide still remains extremely useful.

Here's my notes from the 2017-2018 import process.
 
Importing the flat file into the database hit issues. SSMS showed preview data ok, but would not import the file correctly (it was insisting on trying to convert columns with numbers in, to numeric types, even after specifying the column as nvarchar, then refused to import the file as some rows didn’t contain numbers!)

Instead, here’s how i imported the raw data

* Copy schema of the 2 tables, changing floats to nvarchar, i.e.
    see CreateImportTableForAchievementRatesNational.sql
        CreateImportTableForAchievementRatesProvider.sql

* Import csv files into Excel
    see NART205_201718_NationalAchievementRateTables_Apprenticeship_Overall_SectorSubjectArea.csv
        NART217_201718_NationalAchievementRateTables_Apprenticeship_Overall_Institution_SectorSubjectArea.csv

* Script insert into's. Add (and fill) into excel column, e.g. 
    INSERT INTO [dbo].[NationalRaw] ([Age],[Sector Subject Area Tier 1],[Sector Subject Area Tier 2],[Apprenticeship Level],[Apprenticeship Type],[Overall Cohort],[Overall Achievement Rate %],[Institution Type],[SSA1],[SSA2],[HybridYear]) VALUES(

* Add (and fill formula) into excel column, i.e.
    ="'"&C2&"','"&D2&"','"&E2&"','"&F2&"','"&G2&"','"&H2&"','a"&I2&"','"&A2&"','','','"&B2&"')"
    ="'"&B2&"','"&E2&"','"&F2&"','"&G2&"','"&H2&"','"&I2&"','"&C2&"','"&A2&"','"&J2&"','"&K2&"','','','"&E2&"')"

* Add (and fill) concat column to join insert column & values column

* Do some manual data cleansing, e.g. some strings have single quotes. e.g.
    BARNARDO'S -> BARNARDO''S
    CHEYNE'S => CHEYNE''S
    TRAIN'D UP => TRAIN''D UP
    PALMER'S COLLEGE => PALMER''S COLLEGE

* Copy and paste generated insert statements out of Excel and run in SSMS, into empty deployed database

* Copy from raw imports into destination table, manipulating some fields as per earlier guide / stored procs. e.g.
    see InsertNewAchievementRatesNational.sql
        InsertNewAchievementRatesProvider.sql

* Generate insert statements from national/provider tables (use Tasks -> Generate scripts...)

* Get devops to run insert scripts in test environment & testers to test. if ok, get devops to run insert scripts in pp/prod (etc?)

Whats left to do

Source csv files don't contain any ssa1 or ssa2 data. Need to see if it’s necessary, and if so where/how to get the data).
GetAchievementRatesNational stored proc returns SSA2Code, but not SSA1Code. 
Need to check if SSA2Code is used and what for. Can we do without it? Does the consumer of the stored proc need it to be non-null?

Check generated data in app
