# Digital Apprenticeship Service
## Apprenticeship Programmes Indexer


|               |               |
| ------------- | ------------- |
|![crest] (https://assets.publishing.service.gov.uk/government/assets/crests/org_crest_27px-916806dcf065e7273830577de490d5c7c42f36ddec83e907efe62086785f24fb.png)|Find Apprenticeship Training|
| Build | <img alt="Build Status" src="https://sfa-gov-uk.visualstudio.com/_apis/public/build/definitions/c39e0c0b-7aff-4606-b160-3566f3bbce23/165/badge" /> |
| Web  | https://github.com/SkillsFundingAgency/das-apprenticeship-programs-indexer  |

*Sfa.Das.Sas.Indexer*  
Is responsible of creating a searchable index for apprenticeship and providers 

External dependencies: 
- Data
  - Course Directory for information about providers 
  - LARS (zip) csv files with all standards and all frameworks
- Internal data repository, at the moment a git repository that will be move to use a CMS 
- Elasticsearch to store the data.
