using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider
{
    public interface IProviderDataService
    {
        void SetLearnerSatisfactionRate(IEnumerable<SatisfactionRateProvider> satisfactionRates, Core.Models.Provider.Provider provider);
        void SetEmployerSatisfactionRate(IEnumerable<SatisfactionRateProvider> satisfactionRates, Core.Models.Provider.Provider provider);
        void UpdateStandard(StandardInformation si, List<StandardMetaData> standards, IEnumerable<AchievementRateProvider> achievementRates, IEnumerable<AchievementRateNational> nationalAchievementRates);
        void UpdateFramework(FrameworkInformation fi, List<FrameworkMetaData> frameworks, IEnumerable<AchievementRateProvider> achievementRates, IEnumerable<AchievementRateNational> nationalAchievementRates);
    }
}