using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.DependencyResolution;
using Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution;
using Sfa.Das.Sas.Indexer.Infrastructure.Shared.DependencyResolution;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.AzureWorkerRole.DependencyResolution
{
    public class MediatrRegistry : Registry
    {
        public MediatrRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<ApprenticeshipApplicationServicesRegistry>();
                s.AssemblyContainingType<InfrastructureRegistry>();
                s.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                s.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                s.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                s.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
            });
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();
        }
    }
}