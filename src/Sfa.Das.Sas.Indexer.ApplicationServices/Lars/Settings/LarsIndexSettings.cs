namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings
{
    using System.Configuration;
    using Lars.Services;
    using Shared.Settings;

    public class LarsIndexSettings : IIndexSettings<IMaintainLarsIndex>
    {
        public string IndexesAlias => ConfigurationManager.AppSettings["LarsIndexAlias"];

        public string PauseTime => ConfigurationManager.AppSettings["PauseTime"];

        public string StandardProviderDocumentType => ConfigurationManager.AppSettings["StandardProviderDocumentType"];

        public string FrameworkProviderDocumentType => ConfigurationManager.AppSettings["FrameworkProviderDocumentType"];
        public string FrameworkIdFormat => ConfigurationManager.AppSettings["FrameworkIdFormat"];
    }
}