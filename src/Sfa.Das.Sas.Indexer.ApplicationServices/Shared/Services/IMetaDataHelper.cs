using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;

    public interface IMetaDataHelper
    {
        LarsData GetAllApprenticeshipLarsMetaData();

        AssessmentOrganisationsDTO GetAssessmentOrganisationsData();
    }
}