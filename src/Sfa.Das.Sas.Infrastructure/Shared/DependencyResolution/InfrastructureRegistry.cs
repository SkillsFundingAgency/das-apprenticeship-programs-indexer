using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.DependencyResolution
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using Nest;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
    using Sfa.Das.Sas.Indexer.Infrastructure.Azure;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Elasticsearch;
    using StructureMap;

    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            // Shared
            For<IMessageQueueService>().Use<AzureCloudQueueService>();
            For<IElasticsearchConfiguration>().Use<ElasticsearchConfiguration>();
            For<IElasticsearchSettings>().Use<ElasticsearchSettings>();
            For<IConvertFromCsv>().Use<CsvService>();
            For<IVstsClient>().Use<VstsClient>();
            For<IHttpGetFile>().Use<HttpService>();
            For<IHttpGet>().Use<HttpService>();
            For<IHttpPost>().Use<HttpService>();
            For<IInfrastructureSettings>().Use<InfrastructureSettings>();
            For<ILog>().Use(x => new NLogLogger(x.ParentType, null, GetProperties())).AlwaysUnique();
            For<IUnzipStream>().Use<ZipFileExtractor>();
            For<IElasticsearchMapper>().Use<ElasticsearchMapper>();
            For<IElasticClient>().Use<ElasticClient>();
            For<IElasticsearchCustomClient>().Use<ElasticsearchCustomClient>();
            For<IIndexerServiceFactory>().Use<IndexerServiceFactory>();
            For<IMonitoringService>().Use<MonitoringService>();
        }

        private IDictionary<string, object> GetProperties()
        {
            var properties = new Dictionary<string, object>();
            properties.Add("Version", GetVersion());
            return properties;
        }

        private string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}