using System.Configuration;

namespace Sfa.Deds.Settings
{
    public class LarsSettings : ILarsSettings
    {
        public string SearchEndpointConfigurationName => ConfigurationManager.AppSettings["SearchEndpointConfigurationName"];

        public string DatasetName => ConfigurationManager.AppSettings["DatasetName"];

        public string StandardDescriptorName => ConfigurationManager.AppSettings["StandardDescriptorName"];
    }
}