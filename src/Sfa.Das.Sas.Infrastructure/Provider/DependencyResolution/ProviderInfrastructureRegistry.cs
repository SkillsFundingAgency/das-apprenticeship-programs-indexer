using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.CourseDirectory;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.ElasticSearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using StructureMap;
using Ukrlp.SoapApi.Client;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DependencyResolution
{
    public class ProviderInfrastructureRegistry : Registry
    {
        public ProviderInfrastructureRegistry()
        {
            For<ICourseDirectoryProviderDataService>().Use(x => new CourseDirectoryProviderDataService(x.GetInstance<IInfrastructureSettings>()));
            For<ICourseDirectoryProviderMapper>().Use<CourseDirectoryProviderMapper>();
            For<IMaintainProviderIndex>().Use<ElasticsearchProviderIndexMaintainer>();
            For<IDatabaseProvider>().Use<DatabaseProvider>();
            For<IProviderQueryApiClient>().Use(x => new ProviderQueryApiClient(x.GetInstance<IInfrastructureSettings>().UkrlpServiceEndpointUrl));
            For<IUkrlpProviderMapper>().Use<UkrlpProviderMapper>();
        }
    }
}