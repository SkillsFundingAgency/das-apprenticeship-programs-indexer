using System.Configuration;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Microsoft.Azure;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings
{
    public class StandardIndexSettings : IIndexSettings<IMaintainApprenticeshipIndex>
    {
        public string IndexesAlias => CloudConfigurationManager.GetSetting("ApprenticeshipIndexAlias");

        public string PauseTime => ConfigurationManager.AppSettings["PauseTime"];

        public string StandardProviderDocumentType => ConfigurationManager.AppSettings["StandardProviderDocumentType"];

        public string FrameworkProviderDocumentType => ConfigurationManager.AppSettings["FrameworkProviderDocumentType"];
    }
}