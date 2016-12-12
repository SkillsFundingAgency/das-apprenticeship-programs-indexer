using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IProviderDataService
    {
        void SetLearnerSatisfactionRate(LearnerSatisfactionRateResult satisfactionRates, Core.Models.Provider.Provider provider);
        void SetEmployerSatisfactionRate(EmployerSatisfactionRateResult satisfactionRates, Core.Models.Provider.Provider provider);
        void UpdateStandard(StandardInformation si, StandardMetaDataResult standards, IEnumerable<AchievementRateProvider> achievementRates, AchievementRateNationalResult nationalAchievementRates);
        void UpdateFramework(FrameworkInformation fi, FrameworkMetaDataResult frameworks, IEnumerable<AchievementRateProvider> achievementRates, AchievementRateNationalResult nationalAchievementRates);
        Task<ProviderSourceDto> LoadDatasetsAsync();
    }
}