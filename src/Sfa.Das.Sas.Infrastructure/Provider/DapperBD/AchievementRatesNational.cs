using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DapperBD
{
    public sealed class AchievementRatesNational : IRequestHandler<AchievementRateNationalRequest, AchievementRateNationalResult>
    {
        private readonly IAchievementRatesProvider _provider;

        public AchievementRatesNational(IAchievementRatesProvider provider)
        {
            _provider = provider;
        }

        public AchievementRateNationalResult Handle(AchievementRateNationalRequest message)
        {
            return _provider.GetAllNational();
        }
    }
}