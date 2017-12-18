using System.Collections.Generic;
using System.Linq;
using Dfe.Edubase2.SoapApi.Client;
using Dfe.Edubase2.SoapApi.Client.EdubaseService;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.CourseDirectory
{
    public sealed class HeiProviderVstsClient : IRequestHandler<HeiProvidersRequest, HeiProvidersResult>
    {
        private readonly ILog _logger;

        private readonly IEstablishmentClient _client;

        public HeiProviderVstsClient(
            ILog logger,
            IEstablishmentClient client)
        {
            _logger = logger;
            _client = client;
        }

        public HeiProvidersResult Handle(HeiProvidersRequest message)
        {
            var apiProviders = GetHeiUkprns()
                .ToList();

            _logger.Info($"Found ${apiProviders.Count} provider with establishment type of Higher Education");

            return new HeiProvidersResult { Providers = apiProviders };
        }

        private IEnumerable<string> GetHeiUkprns()
        {
            var page0 = _client.FindEstablishments(new EstablishmentFilter { Page = 0, TypeOfEstablishment = "29", Fields = new StringList { "UKPRN" } });
            return page0.Select(m => m.UKPRN).Where(m => m != null).Distinct().Select(m => m.ToString());
        }
    }
}