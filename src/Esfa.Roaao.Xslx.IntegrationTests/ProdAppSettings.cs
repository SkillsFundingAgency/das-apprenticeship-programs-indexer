using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Esfa.Roaao.Xslx.IntegrationTests
{
    internal class ProdAppSettings : AppServiceSettings, IAppServiceSettings
    {
        public ProdAppSettings(IProvideSettings settingsProvider) : base(settingsProvider)
        {
        }

        public new string VstsAssessmentOrgsUrl => base.VstsAssessmentOrgsUrl.Replace("local", "prod");
    }
}
