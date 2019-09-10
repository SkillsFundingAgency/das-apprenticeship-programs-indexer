# Digital Apprenticeship Service
## Apprenticeship Programmes Indexer


|               |               |
| ------------- | ------------- |
|![crest](https://assets.publishing.service.gov.uk/static/images/govuk-crest-bb9e22aff7881b895c2ceb41d9340804451c474b883f09fe1b4026e76456f44b.png)|Find Apprenticeship Training|
| Build | <img alt="Build Status" src="https://sfa-gov-uk.visualstudio.com/_apis/public/build/definitions/c39e0c0b-7aff-4606-b160-3566f3bbce23/165/badge" /> |
| Web  | https://github.com/SkillsFundingAgency/das-apprenticeship-programs-indexer  |
 
Is responsible of creating a searchable index for apprenticeship and providers 

## Data sources 

### Standards and Frameworks
- LARS export
	- https://hub.fasst.org.uk/Learning%20Aims/Downloads/Pages/default.aspx
- Additional content in Json files stored in a private git repo 

### Providers
- Course Directory
- FCS Active provider list
- UKRLP for provider addresses
- HEI list
- Roatp Service


### Notes for Developers
There has been a recent update to FAT Indexer, so that it now consumes roatp from Roatp-service API instead of MetaDataStorage.  You will require a roatp-service endpoint to consume for FAT Indexer to run locally.  There are two options
1. Setup and run roatp-service locally, concurrently with FAT Indexer
2. Do the following steps to point your local FAT indexer to a working endpoint, in this case AT
Go to ServiceConfiguration.Local.cscfg
and set all the roatp variables to empty
 - &lt;Setting name=&quot;RoatpApiClientBaseUrl&quot; value =&quot;&quot; /&gt;
 - &lt;Setting name=&quot;RoatpApiAuthenticationInstance&quot;  value =&quot;&quot; /&gt;
 - &lt;Setting name=&quot;RoatpApiAuthenticationTenantId&quot;  value =&quot;&quot;/&gt;
 - &lt;Setting name=&quot;RoatpApiAuthenticationClientId&quot;  value =&quot;&quot;/&gt;
 - &lt;Setting name=&quot;RoatpApiAuthenticationClientSecret&quot;  value =&quot;&quot;/&gt;
 - &lt;Setting name=&quot;RoatpApiAuthenticationResourceId&quot;  value =&quot;&quot;/&gt;
 - &lt;Setting name=&quot;RoatpApiAuthenticationApiBaseAddress&quot;  value =&quot;&quot;/&gt;
  
  
Set these up as environmental variables (this assumes you want to use AT as source of roatp data)
- DAS_RoatpApiAuthenticationApiBaseAddress = https://das-at-roatp-as.azurewebsites.net
 - DAS_RoatpApiAuthenticationTenantId = citizenazuresfabisgov.onmicrosoft.com
 - DAS_RoatpApiAuthenticationClientId = 960510a1-87bd-4d98-bde3-00a9646abfdb
 - DAS_RoatpApiAuthenticationClientSecret	  = Get from DevOps and ask for the value for on AT das-apprenticeship-programs-indexer for variable 'RoatpApiAuthenticationClientSecret'
 - DAS_RoatpApiAuthenticationInstance = https://login.microsoftonline.com/
 - DAS_RoatpApiAuthenticationResourceId = https://citizenazuresfabisgov.onmicrosoft.com/das-roatpservice-api
 - DAS_RoatpApiClientBaseUrl = https://at-providers-api.apprenticeships.education.gov.uk
