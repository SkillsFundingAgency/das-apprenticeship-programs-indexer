using System.Configuration;
using FeatureToggle.Core;
using Microsoft.Azure;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.FeatureToggles
{
    public class CloudConfigToggleValueProvider : IBooleanToggleValueProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var setting = ConfigurationManager.AppSettings[$"FeatureToggle.{toggle.GetType().Name}"];

            return !string.IsNullOrEmpty(setting) && bool.Parse(setting);
        }
    }
}
