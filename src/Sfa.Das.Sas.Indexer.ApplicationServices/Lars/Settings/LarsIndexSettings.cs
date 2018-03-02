namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings
{
    using System.Configuration;
    using Microsoft.Azure;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

    public class LarsIndexSettings : IIndexSettings<IMaintainLarsIndex>
    {
        public string IndexesAlias => CloudConfigurationManager.GetSetting("LarsIndexAlias");

        public string PauseTime => ConfigurationManager.AppSettings["PauseTime"];

        public string StandardProviderDocumentType => ConfigurationManager.AppSettings["StandardProviderDocumentType"];

        public string FrameworkProviderDocumentType => ConfigurationManager.AppSettings["FrameworkProviderDocumentType"];
    }
}