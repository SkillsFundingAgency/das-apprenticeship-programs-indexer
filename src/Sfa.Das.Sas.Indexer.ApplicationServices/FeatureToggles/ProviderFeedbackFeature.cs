using FeatureToggle.Core;
using FeatureToggle.Toggles;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.FeatureToggles
{
    public sealed class ProviderFeedbackFeature : SimpleFeatureToggle
    {
        public override IBooleanToggleValueProvider ToggleValueProvider { get => new CloudConfigToggleValueProvider(); set => base.ToggleValueProvider = value; }
    }
}
