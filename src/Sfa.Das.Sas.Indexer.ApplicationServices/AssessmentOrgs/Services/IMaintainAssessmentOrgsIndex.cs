namespace Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

    public interface IMaintainAssessmentOrgsIndex : IMaintainSearchIndexes
    {
        Task IndexStandardOrganisationsData(string indexName, List<StandardOrganisationsData> standardOrganisationsData);
        Task IndexOrganisations(string indexName, List<Organisation> organisations);
    }
}