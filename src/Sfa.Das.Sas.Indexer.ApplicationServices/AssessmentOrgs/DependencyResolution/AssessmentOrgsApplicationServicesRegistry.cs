namespace Sfa.Das.Sas.Indexer.ApplicationServices.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using StructureMap;

    public class AssessmentOrgsApplicationServicesRegistry : Registry
    {
        public AssessmentOrgsApplicationServicesRegistry()
        {
            For<IIndexSettings<IMaintainAssessmentOrgsIndex>>().Use<AssessmentOrgsIndexSettings>();
            For<IIndexerService<IMaintainAssessmentOrgsIndex>>().Use<IndexerService<IMaintainAssessmentOrgsIndex>>();
            For<IGenericIndexerHelper<IMaintainAssessmentOrgsIndex>>().Use<AssessmentOrgsIndexer>();
        }
    }
}