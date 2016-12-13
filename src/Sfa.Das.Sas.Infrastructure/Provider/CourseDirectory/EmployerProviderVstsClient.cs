using MediatR;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public sealed class EmployerProviderVstsClient : IRequestHandler<EmployerProviderRequest, EmployerProviderResult>
    {
        private readonly IGetApprenticeshipProviders _vstsClient;

        public EmployerProviderVstsClient(IGetApprenticeshipProviders vstsClient)
        {
            _vstsClient = vstsClient;
        }

        public EmployerProviderResult Handle(EmployerProviderRequest message)
        {
            return _vstsClient.GetEmployerProviders();
        }
    }
}