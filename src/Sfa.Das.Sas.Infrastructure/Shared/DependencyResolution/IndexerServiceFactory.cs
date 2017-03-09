using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services;
    using StructureMap;

    public class IndexerServiceFactory : IIndexerServiceFactory
    {
        private readonly IContainer _container;

        public IndexerServiceFactory(IContainer container)
        {
            _container = container;
        }

        public IIndexerService<T> GetIndexerService<T>()
        {
            using (var nested = _container.GetNestedContainer())
            {
                nested.Configure(_ =>
                {
                    _.For<ILog>().Use(x => new NLogService<T>(x.ParentType, x.GetInstance<IInfrastructureSettings>()));
                });

                return nested.GetInstance<IIndexerService<T>>();
            }
        }
    }
}
