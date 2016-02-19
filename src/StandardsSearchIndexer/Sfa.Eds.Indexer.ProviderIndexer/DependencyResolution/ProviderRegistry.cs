﻿using Sfa.Eds.Das.ProviderIndexer.Consumers;
using Sfa.Eds.Das.ProviderIndexer.Helpers;
using Sfa.Eds.Das.ProviderIndexer.Services;
using Sfa.Eds.Das.ProviderIndexer.Settings;
using StructureMap;

namespace Sfa.Eds.Das.ProviderIndexer.DependencyResolution
{
    public class ProviderRegistry : Registry
    {
        public ProviderRegistry()
        {
            For<IProviderIndexerService>().Use<ProviderIndexerService>();
            For<IProviderHelper>().Use<ProviderHelper>();
            For<IProviderControlQueueConsumer>().Use<ProviderControlQueueConsumer>();
            For<IProviderIndexSettings>().Use<ProviderIndexSettings>();
        }
    }
}