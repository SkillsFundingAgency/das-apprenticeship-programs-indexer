namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings
{
    using System.Configuration;
    using Microsoft.Azure;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

    public class AssessmentOrgsIndexSettings : IIndexSettings<IMaintainAssessmentOrgsIndex>
    {
        public string IndexesAlias => ConfigurationManager.AppSettings["AssessmentOrgsIndexAlias"];
    }
}