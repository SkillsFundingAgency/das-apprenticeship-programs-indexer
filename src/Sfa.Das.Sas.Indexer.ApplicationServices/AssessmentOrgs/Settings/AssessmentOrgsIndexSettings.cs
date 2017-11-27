namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings
{
    using System.Configuration;
    using AssessmentOrgs.Services;
    using Shared.Settings;

    public class AssessmentOrgsIndexSettings : IIndexSettings<IMaintainAssessmentOrgsIndex>
    {
        public string IndexesAlias => ConfigurationManager.AppSettings["AssessmentOrgsIndexAlias"];

        public string PauseTime => ConfigurationManager.AppSettings["PauseTime"];

        public string StandardProviderDocumentType => ConfigurationManager.AppSettings["StandardProviderDocumentType"];

        public string FrameworkProviderDocumentType => ConfigurationManager.AppSettings["FrameworkProviderDocumentType"];
        public string FrameworkIdFormat => ConfigurationManager.AppSettings["FrameworkIdFormat"];

    }
}