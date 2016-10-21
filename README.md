# DAS - Search and Shortlist

|               |               |
| ------------- | ------------- |
|![crest] (https://assets.publishing.service.gov.uk/government/assets/crests/org_crest_27px-916806dcf065e7273830577de490d5c7c42f36ddec83e907efe62086785f24fb.png)|Find Apprenticeship Training|
| Build | <img alt="Build Status" src="https://sfa-gov-uk.visualstudio.com/DefaultCollection/_apis/public/build/definitions/c39e0c0b-7aff-4606-b160-3566f3bbce23/12/badge" /> |
| Web  | https://github.com/SkillsFundingAgency/das-search  |

This repository contains the code for the [Skills Funding Agency](https://www.gov.uk/government/organisations/skills-funding-agency) service called **Find apprenticeship training** and includes 2 applications.   
The [web application](https://www.findapprenticeship.service.gov.uk) provides a web interface to find apprenticeships and organisations that provide training for them.


=================================
Find Apprenticeship Training Website
=================================
*Sfa.Das.Sas.Web*  
Employer website to search apprenticeship and providers.

External dependencies: 
- [Postcodes.io](http://postcodes.io)
- Searchable index with apprenticeships and providers (Elasticsearch)
- GA and survey monkey 

### Tests
- Unit tests
- UI (selenium) test in separate solution /webtest/DASWebTests.sln
- View test using razor generator

  
Apprenticeship Search Indexer 
=================================
*Sfa.Das.Sas.Indexer*  
Is responsible of creating a searchable index for apprenticeship and providers 

External dependencies: 
- Data
  - Course Directory for information about providers 
  - LARS (zip) csv files with all standards and all frameworks
- Internal data repository, at the moment a git repository that will be move to use a CMS 
- Elasticsearch to store the data.

Apprenticeship API
=================================
*In progress*

## Logging

For logging we are using NLog to log to Elasticsearch. Each log entry should contain at minimum the following properties: 
- **Environment** (local, CI, SystemTest, Demo, PreProd or Production)
- **Application** (Sfa.Das.Sas.Web or Sfa.Das.Sas.Indexer)
- **Message**
- **Level**
