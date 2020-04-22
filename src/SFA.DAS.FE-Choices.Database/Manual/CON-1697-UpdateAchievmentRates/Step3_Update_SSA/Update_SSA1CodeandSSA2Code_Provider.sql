select * from [dbo].[ProviderRow] 
--order by [Sector Subject Area Tier 1]
where [Sector Subject Area Tier 2] != 'All Sector Subject Area Tier 2' and [SSA2 Code] IS NULL

--Agriculture, Horticulture and Animal Care
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care'
order by [SSA2 Code]

update [dbo].[ProviderRow] 
set [SSA1 Code]='3' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care'

update [dbo].[ProviderRow] 
set [SSA2 Code]='3.1' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Agriculture' and [SSA1 Code]='3'

update [dbo].[ProviderRow] 
set [SSA2 Code]='3.3' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Animal Care and Veterinary Science' and [SSA1 Code]='3'

update [dbo].[ProviderRow] 
set [SSA2 Code]='3.4' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Environmental Conservation' and [SSA1 Code]='3'

update [dbo].[ProviderRow] 
set [SSA2 Code]='3.2' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Horticulture and Forestry' and [SSA1 Code]='3'

--Arts, Media and Publishing
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Arts, Media and Publishing' 
order by [Sector Subject Area Tier 2]

select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 2]='Publishing and Information Services'

update [dbo].[ProviderRow] 
set [SSA1 Code]='9' where [Sector Subject Area Tier 1]='Arts, Media and Publishing'

update [dbo].[ProviderRow] 
set [SSA2 Code]='9.2' where [Sector Subject Area Tier 1]='Arts, Media and Publishing' and
[Sector Subject Area Tier 2]='Crafts, Creative Arts and Design' and [SSA1 Code]='9'

update [dbo].[ProviderRow] 
set [SSA2 Code]='9.3' where [Sector Subject Area Tier 1]='Arts, Media and Publishing' and
[Sector Subject Area Tier 2]='Media and Communication' and [SSA1 Code]='9'

update [dbo].[ProviderRow] 
set [SSA2 Code]='9.4' where [Sector Subject Area Tier 1]='Arts, Media and Publishing' and
[Sector Subject Area Tier 2]='Publishing and Information Services' and [SSA1 Code]='9' --0 rows

--Business, Administration and Law
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Business, Administration and Law' 
order by [SSA2 Code]

update [dbo].[ProviderRow] 
set [SSA1 Code]='15' where [Sector Subject Area Tier 1]='Business, Administration and Law'

update [dbo].[ProviderRow] 
set [SSA2 Code]='15.1' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Accounting and Finance' and [SSA1 Code]='15'

update [dbo].[ProviderRow] 
set [SSA2 Code]='15.2' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Administration' and [SSA1 Code]='15'

update [dbo].[ProviderRow] 
set [SSA2 Code]='15.3' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Business Management' and [SSA1 Code]='15'

update [dbo].[ProviderRow] 
set [SSA2 Code]='15.4' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Marketing and Sales' and [SSA1 Code]='15'

update [dbo].[ProviderRow] 
set [SSA2 Code]='15.5' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Law and Legal Services' and [SSA1 Code]='15'

--Construction, Planning and the Built Environment
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Construction, Planning and the Built Environment' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='5' where [Sector Subject Area Tier 1]='Construction, Planning and the Built Environment'

update [dbo].[ProviderRow] 
set [SSA2 Code]='5.2' where [Sector Subject Area Tier 1]='Construction, Planning and the Built Environment' and
[Sector Subject Area Tier 2]='Building and Construction' and [SSA1 Code]='5'

--Education and Training
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Education and Training' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='13' where [Sector Subject Area Tier 1]='Education and Training'

update [dbo].[ProviderRow] 
set [SSA2 Code]='13.2' where [Sector Subject Area Tier 1]='Education and Training' and
[Sector Subject Area Tier 2]='Direct Learning Support' and [SSA1 Code]='13'

update [dbo].[ProviderRow] 
set [SSA2 Code]='13.1' where [Sector Subject Area Tier 1]='Education and Training' and
[Sector Subject Area Tier 2]='Teaching and Lecturing' and [SSA1 Code]='13'

--Engineering and Manufacturing Technologies
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='4' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies'

update [dbo].[ProviderRow] 
set [SSA2 Code]='4.1' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' and
[Sector Subject Area Tier 2]='Engineering' and [SSA1 Code]='4'

update [dbo].[ProviderRow] 
set [SSA2 Code]='4.2' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' and
[Sector Subject Area Tier 2]='Manufacturing Technologies' and [SSA1 Code]='4'

update [dbo].[ProviderRow] 
set [SSA2 Code]='4.3' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' and
[Sector Subject Area Tier 2]='Transportation Operations and Maintenance' and [SSA1 Code]='4'

--Health Public Services and Care
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Health, Public Services and Care' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='1' where [Sector Subject Area Tier 1]='Health, Public Services and Care'

update [dbo].[ProviderRow] 
set [SSA2 Code]='1.5' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Child Development and Well Being' and [SSA1 Code]='1'

update [dbo].[ProviderRow] 
set [SSA2 Code]='1.3' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Health and Social Care' and [SSA1 Code]='1'

update [dbo].[ProviderRow] 
set [SSA2 Code]='1.2' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Nursing and Subjects and Vocations Allied to Medicine' and [SSA1 Code]='1'

update [dbo].[ProviderRow] 
set [SSA2 Code]='1.4' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Public Services' and [SSA1 Code]='1'

update [dbo].[ProviderRow] 
set [SSA2 Code]='1.1' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Medicine and Dentistry' and [SSA1 Code]='1'

--Information and Communication Technology
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Information and Communication Technology' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='6' where [Sector Subject Area Tier 1]='Information and Communication Technology'

update [dbo].[ProviderRow] 
set [SSA2 Code]='6.1' where [Sector Subject Area Tier 1]='Information and Communication Technology' and
[Sector Subject Area Tier 2]='ICT Practitioners' and [SSA1 Code]='6'

update [dbo].[ProviderRow] 
set [SSA2 Code]='6.2' where [Sector Subject Area Tier 1]='Information and Communication Technology' and
[Sector Subject Area Tier 2]='ICT for Users' and [SSA1 Code]='6'

--Leisure, Travel and Tourism
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='8' where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism'

update [dbo].[ProviderRow] 
set [SSA2 Code]='8.1' where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism' and
[Sector Subject Area Tier 2]='Sport, Leisure and Recreation' and [SSA1 Code]='8'

update [dbo].[ProviderRow] 
set [SSA2 Code]='8.2' where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism' and
[Sector Subject Area Tier 2]='Travel and Tourism' and [SSA1 Code]='8'

--Retail and Commercial Enterprise
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='7' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise'

update [dbo].[ProviderRow] 
set [SSA2 Code]='7.4' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Hospitality and Catering' and [SSA1 Code]='7'

update [dbo].[ProviderRow] 
set [SSA2 Code]='7.1' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Retailing and Wholesaling' and [SSA1 Code]='7'

update [dbo].[ProviderRow] 
set [SSA2 Code]='7.3' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Service Enterprises' and [SSA1 Code]='7'

update [dbo].[ProviderRow] 
set [SSA2 Code]='7.2' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Warehousing and Distribution' and [SSA1 Code]='7'

--Science and Mathematics
select * from [dbo].[ProviderRow] where [Sector Subject Area Tier 1]='Science and Mathematics' 
order by [Sector Subject Area Tier 2]

update [dbo].[ProviderRow] 
set [SSA1 Code]='2' where [Sector Subject Area Tier 1]='Science and Mathematics'

update [dbo].[ProviderRow] 
set [SSA2 Code]='2.1' where [Sector Subject Area Tier 1]='Science and Mathematics' and
[Sector Subject Area Tier 2]='Science' and [SSA1 Code]='2'

