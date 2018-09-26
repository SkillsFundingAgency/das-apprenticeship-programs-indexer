using FeatureToggle.Core;
using Microsoft.Azure;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.FeatureToggles
{
    public class CloudConfigToggleValueProvider : IBooleanToggleValueProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var setting = CloudConfigurationManager.GetSetting($"FeatureToggle.{toggle.GetType().Name}");

            return !string.IsNullOrEmpty(setting) && bool.Parse(setting);
        }
    }
}
