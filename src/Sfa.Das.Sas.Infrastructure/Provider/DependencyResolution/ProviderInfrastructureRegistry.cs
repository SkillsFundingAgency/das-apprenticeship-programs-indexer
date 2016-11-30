using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory;
using Sfa.Das.Sas.Indexer.Infrastructure.DapperBD;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution
{
    public class ProviderInfrastructureRegistry : Registry
    {
        public ProviderInfrastructureRegistry()
        {
            For<IGetActiveProviders>().Use<FcsActiveProvidersClient>();
            For<ICourseDirectoryProviderDataService>().Use(x => new CourseDirectoryProviderDataService(x.GetInstance<IInfrastructureSettings>()));
            For<ILogProvider>().Use(x => new NLogProviderService(x.ParentType, x.GetInstance<IInfrastructureSettings>())).AlwaysUnique();
            For<IGetCourseDirectoryProviders>().Use<CourseDirectoryClient>();
            For<ICourseDirectoryProviderMapper>().Use<CourseDirectoryProviderMapper>();
            For<IGetApprenticeshipProviders>().Use<ProviderVstsClient>();
            For<IMaintainProviderIndex>().Use<ElasticsearchProviderIndexMaintainer>();
            For<IDatabaseProvider>().Use<DatabaseProvider>();
            For<IAchievementRatesProvider>().Use<AchievementRatesProvider>();
            For<ISatisfactionRatesProvider>().Use<SatisfactionRatesProvider>();
            For<IUkrlpService>().Use<UkrlpService>();
            For<IUkrlpClient>().Use<UkrlpClient>();
            For<IUkrlpProviderResponseMapper>().Use<UkrlpProviderResponseMapper>();
            For<IUkrlpProviderMapper>().Use<UkrlpProviderMapper>();
        }
    }
}