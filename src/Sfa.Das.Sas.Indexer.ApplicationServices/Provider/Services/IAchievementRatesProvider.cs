using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IAchievementRatesProvider
    {
        AchievementRateProviderResult GetAllByProvider();

        AchievementRateNationalResult GetAllNational();
    }
}
