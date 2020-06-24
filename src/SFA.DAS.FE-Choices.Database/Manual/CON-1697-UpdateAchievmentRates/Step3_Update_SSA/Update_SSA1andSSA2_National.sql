select * from [dbo].[NationalRaw] order by [Sector Subject Area Tier 1]

select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' 

--Agriculture, Horticulture and Animal Care
update [dbo].[NationalRaw] 
set SSA1='3' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care'

update [dbo].[NationalRaw] 
set SSA2='3.1' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Agriculture'

update [dbo].[NationalRaw] 
set SSA2='3.3' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Animal Care and Veterinary Science'

update [dbo].[NationalRaw] 
set SSA2='3.4' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Environmental Conservation'

update [dbo].[NationalRaw] 
set SSA2='3.2' where [Sector Subject Area Tier 1]='Agriculture, Horticulture and Animal Care' and
[Sector Subject Area Tier 2]='Horticulture and Forestry'

--Arts, Media and Publishing
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Arts, Media and Publishing' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='9' where [Sector Subject Area Tier 1]='Arts, Media and Publishing'

update [dbo].[NationalRaw] 
set SSA2='9.2' where [Sector Subject Area Tier 1]='Arts, Media and Publishing' and
[Sector Subject Area Tier 2]='Crafts, Creative Arts and Design'

update [dbo].[NationalRaw] 
set SSA2='9.3' where [Sector Subject Area Tier 1]='Arts, Media and Publishing' and
[Sector Subject Area Tier 2]='Media and Communication'

update [dbo].[NationalRaw] 
set SSA2='9.4' where [Sector Subject Area Tier 1]='Arts, Media and Publishing' and
[Sector Subject Area Tier 2]='Publishing and Information Services'

--Business, Administration and Law
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Business, Administration and Law' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='15' where [Sector Subject Area Tier 1]='Business, Administration and Law'

update [dbo].[NationalRaw] 
set SSA2='15.1' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Accounting and Finance'

update [dbo].[NationalRaw] 
set SSA2='15.2' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Administration'

update [dbo].[NationalRaw] 
set SSA2='15.3' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Business Management'

update [dbo].[NationalRaw] 
set SSA2='15.4' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Marketing and Sales'

update [dbo].[NationalRaw] 
set SSA2='15.5' where [Sector Subject Area Tier 1]='Business, Administration and Law' and
[Sector Subject Area Tier 2]='Law and Legal Services'

--Construction, Planning and the Built Environment
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Construction, Planning and the Built Environment' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='5' where [Sector Subject Area Tier 1]='Construction, Planning and the Built Environment'

update [dbo].[NationalRaw] 
set SSA2='5.2' where [Sector Subject Area Tier 1]='Construction, Planning and the Built Environment' and
[Sector Subject Area Tier 2]='Building and Construction' and SSA1='5'

--Education and Training
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Education and Training' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='13' where [Sector Subject Area Tier 1]='Education and Training'

update [dbo].[NationalRaw] 
set SSA2='13.2' where [Sector Subject Area Tier 1]='Education and Training' and
[Sector Subject Area Tier 2]='Direct Learning Support' and SSA1='13'

update [dbo].[NationalRaw] 
set SSA2='13.1' where [Sector Subject Area Tier 1]='Education and Training' and
[Sector Subject Area Tier 2]='Teaching and Lecturing' and SSA1='13'

--Engineering and Manufacturing Technologies
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='4' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies'

update [dbo].[NationalRaw] 
set SSA2='4.1' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' and
[Sector Subject Area Tier 2]='Engineering' and SSA1='4'

update [dbo].[NationalRaw] 
set SSA2='4.2' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' and
[Sector Subject Area Tier 2]='Manufacturing Technologies' and SSA1='4'

update [dbo].[NationalRaw] 
set SSA2='4.3' where [Sector Subject Area Tier 1]='Engineering and Manufacturing Technologies' and
[Sector Subject Area Tier 2]='Transportation Operations and Maintenance' and SSA1='4'

--Health Public Services and Care
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Health, Public Services and Care' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='1' where [Sector Subject Area Tier 1]='Health, Public Services and Care'

update [dbo].[NationalRaw] 
set SSA2='1.5' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Child Development and Well Being' and SSA1='1'

update [dbo].[NationalRaw] 
set SSA2='1.3' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Health and Social Care' and SSA1='1'

update [dbo].[NationalRaw] 
set SSA2='1.2' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Nursing and Subjects and Vocations Allied to Medicine' and SSA1='1'

update [dbo].[NationalRaw] 
set SSA2='1.4' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Public Services' and SSA1='1'

update [dbo].[NationalRaw] 
set SSA2='1.1' where [Sector Subject Area Tier 1]='Health, Public Services and Care' and
[Sector Subject Area Tier 2]='Medicine and Dentistry' and SSA1='1'

--Information and Communication Technology
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Information and Communication Technology' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='6' where [Sector Subject Area Tier 1]='Information and Communication Technology'

update [dbo].[NationalRaw] 
set SSA2='6.1' where [Sector Subject Area Tier 1]='Information and Communication Technology' and
[Sector Subject Area Tier 2]='ICT Practitioners' and SSA1='6'

update [dbo].[NationalRaw] 
set SSA2='6.2' where [Sector Subject Area Tier 1]='Information and Communication Technology' and
[Sector Subject Area Tier 2]='ICT for Users' and SSA1='6'

--Leisure, Travel and Tourism
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='8' where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism'

update [dbo].[NationalRaw] 
set SSA2='8.1' where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism' and
[Sector Subject Area Tier 2]='Sport, Leisure and Recreation' and SSA1='8'

update [dbo].[NationalRaw] 
set SSA2='8.2' where [Sector Subject Area Tier 1]='Leisure, Travel and Tourism' and
[Sector Subject Area Tier 2]='Travel and Tourism' and SSA1='8'

--Retail and Commercial Enterprise
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='7' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise'

update [dbo].[NationalRaw] 
set SSA2='7.4' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Hospitality and Catering' and SSA1='7'

update [dbo].[NationalRaw] 
set SSA2='7.1' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Retailing and Wholesaling' and SSA1='7'

update [dbo].[NationalRaw] 
set SSA2='7.3' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Service Enterprises' and SSA1='7'

update [dbo].[NationalRaw] 
set SSA2='7.2' where [Sector Subject Area Tier 1]='Retail and Commercial Enterprise' and
[Sector Subject Area Tier 2]='Warehousing and Distribution' and SSA1='7'

--Science and Mathematics
select * from [dbo].[NationalRaw] where [Sector Subject Area Tier 1]='Science and Mathematics' 
order by [Sector Subject Area Tier 2]

update [dbo].[NationalRaw] 
set SSA1='2' where [Sector Subject Area Tier 1]='Science and Mathematics'

update [dbo].[NationalRaw] 
set SSA2='2.1' where [Sector Subject Area Tier 1]='Science and Mathematics' and
[Sector Subject Area Tier 2]='Science' and SSA1='2'

