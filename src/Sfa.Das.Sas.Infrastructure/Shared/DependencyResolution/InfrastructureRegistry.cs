using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Azure;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
using Sfa.Das.Sas.Indexer.Infrastructure.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services;
using SFA.DAS.NLog.Logger;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.DependencyResolution
{
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
            For<ILog>().Use(x => new NLogService(x.ParentType, x.GetInstance<IInfrastructureSettings>())).AlwaysUnique();
            For<IUnzipStream>().Use<ZipFileExtractor>();
            For<IElasticsearchMapper>().Use<ElasticsearchMapper>();
            For<IElasticClient>().Use<ElasticClient>();
            For<IElasticsearchCustomClient>().Use<ElasticsearchCustomClient>();
            For<IIndexerServiceFactory>().Use<IndexerServiceFactory>();
        }
    }
}