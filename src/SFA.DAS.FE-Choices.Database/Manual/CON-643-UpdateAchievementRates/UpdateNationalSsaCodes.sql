update [AchievementRatesNational] set SSA1 = 1 where [Sector Subject Area Tier 1] = 'Health, Public Services and Care'
update [AchievementRatesNational] set SSA1 = 2 where [Sector Subject Area Tier 1] = 'Science and Mathematics'
update [AchievementRatesNational] set SSA1 = 3 where [Sector Subject Area Tier 1] = 'Agriculture, Horticulture and Animal Care'
update [AchievementRatesNational] set SSA1 = 4 where [Sector Subject Area Tier 1] = 'Engineering and Manufacturing Technologies'
update [AchievementRatesNational] set SSA1 = 5 where [Sector Subject Area Tier 1] = 'Construction, Planning and the Built Environment'
update [AchievementRatesNational] set SSA1 = 6 where [Sector Subject Area Tier 1] = 'Information and Communication Technology'
update [AchievementRatesNational] set SSA1 = 7 where [Sector Subject Area Tier 1] = 'Retail and Commercial Enterprise'
update [AchievementRatesNational] set SSA1 = 8 where [Sector Subject Area Tier 1] = 'Leisure, Travel and Tourism'
update [AchievementRatesNational] set SSA1 = 9 where [Sector Subject Area Tier 1] = 'Arts, Media and Publishing'
update [AchievementRatesNational] set SSA1 = 10 where [Sector Subject Area Tier 1] = 'History, Philosophy and Theology'
update [AchievementRatesNational] set SSA1 = 11 where [Sector Subject Area Tier 1] = 'Social Sciences'
update [AchievementRatesNational] set SSA1 = 12 where [Sector Subject Area Tier 1] = 'Languages, Literature and Culture'
update [AchievementRatesNational] set SSA1 = 13 where [Sector Subject Area Tier 1] = 'Education and Training'
update [AchievementRatesNational] set SSA1 = 14 where [Sector Subject Area Tier 1] = 'Preparation for Life and Work'
update [AchievementRatesNational] set SSA1 = 15 where [Sector Subject Area Tier 1] = 'Business, Administration and Law'
go

update [AchievementRatesNational] set SSA2 = 1.1 where [Sector Subject Area Tier 2] = 'Medicine and Dentistry'
update [AchievementRatesNational] set SSA2 = 1.2 where [Sector Subject Area Tier 2] = 'Nursing and Subjects and Vocations Allied to Medicine'
update [AchievementRatesNational] set SSA2 = 1.3 where [Sector Subject Area Tier 2] = 'Health and Social Care'
update [AchievementRatesNational] set SSA2 = 1.4 where [Sector Subject Area Tier 2] = 'Public Services'
update [AchievementRatesNational] set SSA2 = 1.5 where [Sector Subject Area Tier 2] = 'Child Development and Well Being'

update [AchievementRatesNational] set SSA2 = 2.1 where [Sector Subject Area Tier 2] = 'Science'
-- no National `Mathematics and Statistics` achievement rates? perhaps no-one apprentices in maths!? checked with...
--   select distinct [Sector Subject Area Tier 2]
--     FROM [SFA.DAS.FE-Choices.Database].[dbo].[AchievementRatesNational]
--     where [Sector Subject Area Tier 1] = 'Science and Mathematics'
update [AchievementRatesNational] set SSA2 = 2.2 where [Sector Subject Area Tier 2] = 'Mathematics and Statistics'

update [AchievementRatesNational] set SSA2 = 3.1 where [Sector Subject Area Tier 2] = 'Agriculture'
update [AchievementRatesNational] set SSA2 = 3.2 where [Sector Subject Area Tier 2] = 'Horticulture and Forestry'
update [AchievementRatesNational] set SSA2 = 3.3 where [Sector Subject Area Tier 2] = 'Animal Care and Veterinary Science'
update [AchievementRatesNational] set SSA2 = 3.4 where [Sector Subject Area Tier 2] = 'Environmental Conservation'

update [AchievementRatesNational] set SSA2 = 4.1 where [Sector Subject Area Tier 2] = 'Engineering'
update [AchievementRatesNational] set SSA2 = 4.2 where [Sector Subject Area Tier 2] = 'Manufacturing Technologies'
update [AchievementRatesNational] set SSA2 = 4.3 where [Sector Subject Area Tier 2] = 'Transportation Operations and Maintenance'

-- note, no 'Architecture' or '' either
--   select distinct [Sector Subject Area Tier 2]
--     FROM [SFA.DAS.FE-Choices.Database].[dbo].[AchievementRatesNational]
--     where [Sector Subject Area Tier 1] like 'Construction, Planning and the Built Environment'
update [AchievementRatesNational] set SSA2 = 5.1 where [Sector Subject Area Tier 2] = 'Architecture'
update [AchievementRatesNational] set SSA2 = 5.2 where [Sector Subject Area Tier 2] = 'Building and Construction'
update [AchievementRatesNational] set SSA2 = 5.3 where [Sector Subject Area Tier 2] = 'Urban, Rural and Regional Planning'

update [AchievementRatesNational] set SSA2 = 6.1 where [Sector Subject Area Tier 2] = 'ICT Practitioners'
update [AchievementRatesNational] set SSA2 = 6.2 where [Sector Subject Area Tier 2] = 'ICT for Users'

update [AchievementRatesNational] set SSA2 = 7.1 where [Sector Subject Area Tier 2] = 'Retailing and Wholesaling'
update [AchievementRatesNational] set SSA2 = 7.2 where [Sector Subject Area Tier 2] = 'Warehousing and Distribution'
update [AchievementRatesNational] set SSA2 = 7.3 where [Sector Subject Area Tier 2] = 'Service Enterprises'
update [AchievementRatesNational] set SSA2 = 7.4 where [Sector Subject Area Tier 2] = 'Hospitality and Catering'

update [AchievementRatesNational] set SSA2 = 8.1 where [Sector Subject Area Tier 2] = 'Sport, Leisure and Recreation'
update [AchievementRatesNational] set SSA2 = 8.2 where [Sector Subject Area Tier 2] = 'Travel and Tourism'

update [AchievementRatesNational] set SSA2 = 9.1 where [Sector Subject Area Tier 2] = 'Performing Arts'
update [AchievementRatesNational] set SSA2 = 9.2 where [Sector Subject Area Tier 2] = 'Crafts, Creative Arts and Design'
update [AchievementRatesNational] set SSA2 = 9.3 where [Sector Subject Area Tier 2] = 'Media and Communication'
update [AchievementRatesNational] set SSA2 = 9.4 where [Sector Subject Area Tier 2] = 'Publishing and Information Services'

-- None of these.. (Tier 1 = `History, Philosophy and Theology`)
update [AchievementRatesNational] set SSA2 = 10.1 where [Sector Subject Area Tier 2] = 'History'
update [AchievementRatesNational] set SSA2 = 10.2 where [Sector Subject Area Tier 2] = 'Archaeology and Archaeological Sciences'
update [AchievementRatesNational] set SSA2 = 10.3 where [Sector Subject Area Tier 2] = 'Philosophy'
update [AchievementRatesNational] set SSA2 = 10.4 where [Sector Subject Area Tier 2] = 'Theology and Religious Studies'

-- None of these.. (Tier 1 = `Social Sciences`)
update [AchievementRatesNational] set SSA2 = 11.1 where [Sector Subject Area Tier 2] = 'Geography'
update [AchievementRatesNational] set SSA2 = 11.2 where [Sector Subject Area Tier 2] = 'Sociology and Social Policy'
update [AchievementRatesNational] set SSA2 = 11.3 where [Sector Subject Area Tier 2] = 'Politics'
update [AchievementRatesNational] set SSA2 = 11.4 where [Sector Subject Area Tier 2] = 'Economics'
update [AchievementRatesNational] set SSA2 = 11.4 where [Sector Subject Area Tier 2] = 'Anthropology'

-- None of these.. (Tier 1 = 'Languages, Literature and Culture')
update [AchievementRatesNational] set SSA2 = 12.1 where [Sector Subject Area Tier 2] = 'Languages, Literature and Culture of the British Isles'
update [AchievementRatesNational] set SSA2 = 12.2 where [Sector Subject Area Tier 2] = 'Other Languages, Literature and Culture'
update [AchievementRatesNational] set SSA2 = 12.3 where [Sector Subject Area Tier 2] = 'Linguistics'

update [AchievementRatesNational] set SSA2 = 13.1 where [Sector Subject Area Tier 2] = 'Teaching and Lecturing'
update [AchievementRatesNational] set SSA2 = 13.2 where [Sector Subject Area Tier 2] = 'Direct Learning Support'

-- None of these.. (Tier 1 = 'Preparation for Life and Work')
update [AchievementRatesNational] set SSA2 = 14.1 where [Sector Subject Area Tier 2] = 'Foundations for Learning and Life'
update [AchievementRatesNational] set SSA2 = 14.2 where [Sector Subject Area Tier 2] = 'Preparation for Work'

update [AchievementRatesNational] set SSA2 = 15.1 where [Sector Subject Area Tier 2] = 'Accounting and Finance'
update [AchievementRatesNational] set SSA2 = 15.2 where [Sector Subject Area Tier 2] = 'Administration'
update [AchievementRatesNational] set SSA2 = 15.3 where [Sector Subject Area Tier 2] = 'Business Management'
update [AchievementRatesNational] set SSA2 = 15.4 where [Sector Subject Area Tier 2] = 'Marketing and Sales'
update [AchievementRatesNational] set SSA2 = 15.4 where [Sector Subject Area Tier 2] = 'Law and Legal Services'
go

--TODO: need to check all the missing entries!