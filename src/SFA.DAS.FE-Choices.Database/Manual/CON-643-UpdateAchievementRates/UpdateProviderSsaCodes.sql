update [AchievementRatesProvider] set [SSA1 Code] = 1, [SSA2 Code] = 1.1 where [Sector Subject Area Tier 2] = 'Medicine and Dentistry'
update [AchievementRatesProvider] set [SSA1 Code] = 1, [SSA2 Code] = 1.2 where [Sector Subject Area Tier 2] = 'Nursing and Subjects and Vocations Allied to Medicine'
update [AchievementRatesProvider] set [SSA1 Code] = 1, [SSA2 Code] = 1.3 where [Sector Subject Area Tier 2] = 'Health and Social Care'
update [AchievementRatesProvider] set [SSA1 Code] = 1, [SSA2 Code] = 1.4 where [Sector Subject Area Tier 2] = 'Public Services'
update [AchievementRatesProvider] set [SSA1 Code] = 1, [SSA2 Code] = 1.5 where [Sector Subject Area Tier 2] = 'Child Development and Well Being'

update [AchievementRatesProvider] set [SSA1 Code] = 2, [SSA2 Code] = 2.1 where [Sector Subject Area Tier 2] = 'Science'
-- no National `Mathematics and Statistics` achievement rates? perhaps no-one apprentices in maths!? checked with...
--   select distinct [Sector Subject Area Tier 2]
--     FROM [SFA.DAS.FE-Choices.Database].[dbo].[AchievementRatesProvider]
--     where [Sector Subject Area Tier 1] = 'Science and Mathematics'
update [AchievementRatesProvider] set [SSA1 Code] = 2, [SSA2 Code] = 2.2 where [Sector Subject Area Tier 2] = 'Mathematics and Statistics'

update [AchievementRatesProvider] set [SSA1 Code] = 3, [SSA2 Code] = 3.1 where [Sector Subject Area Tier 2] = 'Agriculture'
update [AchievementRatesProvider] set [SSA1 Code] = 3, [SSA2 Code] = 3.2 where [Sector Subject Area Tier 2] = 'Horticulture and Forestry'
update [AchievementRatesProvider] set [SSA1 Code] = 3, [SSA2 Code] = 3.3 where [Sector Subject Area Tier 2] = 'Animal Care and Veterinary Science'
update [AchievementRatesProvider] set [SSA1 Code] = 3, [SSA2 Code] = 3.4 where [Sector Subject Area Tier 2] = 'Environmental Conservation'

update [AchievementRatesProvider] set [SSA1 Code] = 4, [SSA2 Code] = 4.1 where [Sector Subject Area Tier 2] = 'Engineering'
update [AchievementRatesProvider] set [SSA1 Code] = 4, [SSA2 Code] = 4.2 where [Sector Subject Area Tier 2] = 'Manufacturing Technologies'
update [AchievementRatesProvider] set [SSA1 Code] = 4, [SSA2 Code] = 4.3 where [Sector Subject Area Tier 2] = 'Transportation Operations and Maintenance'

-- note, no 'Architecture' or '' either
--   select distinct [Sector Subject Area Tier 2]
--     FROM [SFA.DAS.FE-Choices.Database].[dbo].[AchievementRatesProvider]
--     where [Sector Subject Area Tier 1] like 'Construction, Planning and the Built Environment'
update [AchievementRatesProvider] set [SSA1 Code] = 5, [SSA2 Code] = 5.1 where [Sector Subject Area Tier 2] = 'Architecture'
update [AchievementRatesProvider] set [SSA1 Code] = 5, [SSA2 Code] = 5.2 where [Sector Subject Area Tier 2] = 'Building and Construction'
update [AchievementRatesProvider] set [SSA1 Code] = 5, [SSA2 Code] = 5.3 where [Sector Subject Area Tier 2] = 'Urban, Rural and Regional Planning'

update [AchievementRatesProvider] set [SSA1 Code] = 6, [SSA2 Code] = 6.1 where [Sector Subject Area Tier 2] = 'ICT Practitioners'
update [AchievementRatesProvider] set [SSA1 Code] = 6, [SSA2 Code] = 6.2 where [Sector Subject Area Tier 2] = 'ICT for Users'

update [AchievementRatesProvider] set [SSA1 Code] = 7, [SSA2 Code] = 7.1 where [Sector Subject Area Tier 2] = 'Retailing and Wholesaling'
update [AchievementRatesProvider] set [SSA1 Code] = 7, [SSA2 Code] = 7.2 where [Sector Subject Area Tier 2] = 'Warehousing and Distribution'
update [AchievementRatesProvider] set [SSA1 Code] = 7, [SSA2 Code] = 7.3 where [Sector Subject Area Tier 2] = 'Service Enterprises'
update [AchievementRatesProvider] set [SSA1 Code] = 7, [SSA2 Code] = 7.4 where [Sector Subject Area Tier 2] = 'Hospitality and Catering'

update [AchievementRatesProvider] set [SSA1 Code] = 8, [SSA2 Code] = 8.1 where [Sector Subject Area Tier 2] = 'Sport, Leisure and Recreation'
update [AchievementRatesProvider] set [SSA1 Code] = 8, [SSA2 Code] = 8.2 where [Sector Subject Area Tier 2] = 'Travel and Tourism'

update [AchievementRatesProvider] set [SSA1 Code] = 9, [SSA2 Code] = 9.1 where [Sector Subject Area Tier 2] = 'Performing Arts'
update [AchievementRatesProvider] set [SSA1 Code] = 9, [SSA2 Code] = 9.2 where [Sector Subject Area Tier 2] = 'Crafts, Creative Arts and Design'
update [AchievementRatesProvider] set [SSA1 Code] = 9, [SSA2 Code] = 9.3 where [Sector Subject Area Tier 2] = 'Media and Communication'
update [AchievementRatesProvider] set [SSA1 Code] = 9, [SSA2 Code] = 9.4 where [Sector Subject Area Tier 2] = 'Publishing and Information Services'

-- None of these.. (Tier 1 = `History, Philosophy and Theology`)
update [AchievementRatesProvider] set [SSA1 Code] = 10, [SSA2 Code] = 10.1 where [Sector Subject Area Tier 2] = 'History'
update [AchievementRatesProvider] set [SSA1 Code] = 10, [SSA2 Code] = 10.2 where [Sector Subject Area Tier 2] = 'Archaeology and Archaeological Sciences'
update [AchievementRatesProvider] set [SSA1 Code] = 10, [SSA2 Code] = 10.3 where [Sector Subject Area Tier 2] = 'Philosophy'
update [AchievementRatesProvider] set [SSA1 Code] = 10, [SSA2 Code] = 10.4 where [Sector Subject Area Tier 2] = 'Theology and Religious Studies'

-- None of these.. (Tier 1 = `Social Sciences`)
update [AchievementRatesProvider] set [SSA1 Code] = 11, [SSA2 Code] = 11.1 where [Sector Subject Area Tier 2] = 'Geography'
update [AchievementRatesProvider] set [SSA1 Code] = 11, [SSA2 Code] = 11.2 where [Sector Subject Area Tier 2] = 'Sociology and Social Policy'
update [AchievementRatesProvider] set [SSA1 Code] = 11, [SSA2 Code] = 11.3 where [Sector Subject Area Tier 2] = 'Politics'
update [AchievementRatesProvider] set [SSA1 Code] = 11, [SSA2 Code] = 11.4 where [Sector Subject Area Tier 2] = 'Economics'
update [AchievementRatesProvider] set [SSA1 Code] = 11, [SSA2 Code] = 11.4 where [Sector Subject Area Tier 2] = 'Anthropology'

-- None of these.. (Tier 1 = 'Languages, Literature and Culture')
update [AchievementRatesProvider] set [SSA1 Code] = 12, [SSA2 Code] = 12.1 where [Sector Subject Area Tier 2] = 'Languages, Literature and Culture of the British Isles'
update [AchievementRatesProvider] set [SSA1 Code] = 12, [SSA2 Code] = 12.2 where [Sector Subject Area Tier 2] = 'Other Languages, Literature and Culture'
update [AchievementRatesProvider] set [SSA1 Code] = 12, [SSA2 Code] = 12.3 where [Sector Subject Area Tier 2] = 'Linguistics'

update [AchievementRatesProvider] set [SSA1 Code] = 13, [SSA2 Code] = 13.1 where [Sector Subject Area Tier 2] = 'Teaching and Lecturing'
update [AchievementRatesProvider] set [SSA1 Code] = 13, [SSA2 Code] = 13.2 where [Sector Subject Area Tier 2] = 'Direct Learning Support'

-- None of these.. (Tier 1 = 'Preparation for Life and Work')
update [AchievementRatesProvider] set [SSA1 Code] = 14, [SSA2 Code] = 14.1 where [Sector Subject Area Tier 2] = 'Foundations for Learning and Life'
update [AchievementRatesProvider] set [SSA1 Code] = 14, [SSA2 Code] = 14.2 where [Sector Subject Area Tier 2] = 'Preparation for Work'

update [AchievementRatesProvider] set [SSA1 Code] = 15, [SSA2 Code] = 15.1 where [Sector Subject Area Tier 2] = 'Accounting and Finance'
update [AchievementRatesProvider] set [SSA1 Code] = 15, [SSA2 Code] = 15.2 where [Sector Subject Area Tier 2] = 'Administration'
update [AchievementRatesProvider] set [SSA1 Code] = 15, [SSA2 Code] = 15.3 where [Sector Subject Area Tier 2] = 'Business Management'
update [AchievementRatesProvider] set [SSA1 Code] = 15, [SSA2 Code] = 15.4 where [Sector Subject Area Tier 2] = 'Marketing and Sales'
update [AchievementRatesProvider] set [SSA1 Code] = 15, [SSA2 Code] = 15.4 where [Sector Subject Area Tier 2] = 'Law and Legal Services'

--TODO: need to check all the missing entries!