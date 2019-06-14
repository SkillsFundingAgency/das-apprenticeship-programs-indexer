using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dfe.Edubase2.SoapApi.Client;
using Dfe.Edubase2.SoapApi.Client.EdubaseService;
using MediatR;

using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Handlers
{
    public sealed class HeiProviderHandler : IAsyncRequestHandler<HeiProvidersRequest, HeiProvidersResult>
    {
        private readonly ILog _logger;

        private readonly IEstablishmentClient _client;

        public HeiProviderHandler(
            ILog logger,
            IEstablishmentClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<HeiProvidersResult> Handle(HeiProvidersRequest message)
        {
            var apiProviders = new List<string>(); // (await GetHeiUkprns()).ToList();

            _logger.Info($"Found ${apiProviders.Count} provider with establishment type of Higher Education");

            return new HeiProvidersResult { Providers = apiProviders };
        }

        private async Task<IEnumerable<string>> GetHeiUkprns()
        {
            var establishments = await _client.FindEstablishmentsAsync(
                new EstablishmentFilter
                    {
                        Page = 0,
                        TypeOfEstablishment = EstablishmentType.HigherEducationInstitutions,
                        Fields = new StringList { "UKPRN" }
                    });

            return establishments.Select(m => m.UKPRN).Where(m => m != null).Distinct().Select(m => m.ToString());
        }
    }
}