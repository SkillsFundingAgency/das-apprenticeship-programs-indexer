using Microsoft.ApplicationInsights.DataContracts;

namespace Sfa.Das.Sas.Web
{
    public class ApplicationInsightsInitializer : Microsoft.ApplicationInsights.Extensibility.IContextInitializer
    {
        public void Initialize(TelemetryContext context)
        {
            context.Properties["Application"] = "Sfa.Das.Web";
        }
    }
}