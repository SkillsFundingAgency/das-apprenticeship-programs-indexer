using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Azure;
using Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.Infrastructure.DapperBD;

    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<IMessageQueueService>().Use<AzureCloudQueueService>();
            For<ILarsSettings>().Use<LarsSettings>();
            For<IElasticsearchConfiguration>().Use<ElasticsearchConfiguration>();
            For<IElasticsearchSettings>().Use<ElasticsearchSettings>();
            For<IGetActiveProviders>().Use<FcsActiveProvidersClient>();
            For<IConvertFromCsv>().Use<CsvService>();
            For<IVstsClient>().Use<VstsClient>();
            For<IHttpGetFile>().Use<HttpService>();
            For<IHttpGet>().Use<HttpService>();
            For<IHttpPost>().Use<HttpService>();
            For<IInfrastructureSettings>().Use<InfrastructureSettings>();
            For<ICourseDirectoryProviderDataService>().Use(x => new CourseDirectoryProviderDataService());
            For<ILog>().Use(x => new NLogService(x.ParentType, x.GetInstance<IInfrastructureSettings>())).AlwaysUnique();
            For<IUnzipStream>().Use<ZipFileExtractor>();
            For<IGetCourseDirectoryProviders>().Use<CourseDirectoryClient>();
            For<ICourseDirectoryProviderMapper>().Use<CourseDirectoryProviderMapper>();
            For<IGetApprenticeshipProviders>().Use<ProviderVstsClient>();
            For<IMaintainApprenticeshipIndex>().Use<ElasticsearchApprenticeshipIndexMaintainer>();
            For<IMaintainProviderIndex>().Use<ElasticsearchProviderIndexMaintainer>();
            For<IElasticsearchMapper>().Use<ElasticsearchMapper>();
            For<IElasticClient>().Use<ElasticClient>();
            For<IElasticsearchCustomClient>().Use<ElasticsearchCustomClient>();
            For<IDatabaseProvider>().Use<DatabaseProvider>();
            For<IAchievementRatesProvider>().Use<AchievementRatesProvider>();
            For<ISatisfactionRatesProvider>().Use<SatisfactionRatesProvider>();
            For<IUkrlpService>().Use<UkrlpService>();
            For<IUkrlpClient>().Use<UkrlpClient>();
            For<IUkrlpProviderWrapperMapper>().Use<UkrlpProviderWrapperMapper>();
        }
    }
}