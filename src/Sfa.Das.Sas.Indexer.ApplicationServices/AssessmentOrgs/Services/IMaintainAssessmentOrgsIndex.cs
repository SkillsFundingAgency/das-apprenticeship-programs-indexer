namespace Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

    public interface IMaintainAssessmentOrgsIndex : IMaintainSearchIndexes
    {
        void IndexStandardOrganisationsData(string indexName, List<StandardOrganisationsData> standardOrganisationsData);
        void IndexOrganisations(string indexName, List<Organisation> organisations);
    }
}