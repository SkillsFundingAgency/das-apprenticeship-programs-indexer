using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class ProviderSourceDto
    {
        public IEnumerable<string> EmployerProviders { get; set; }
        public IEnumerable<Models.CourseDirectory.Provider> CourseDirectoryProviders { get; set; }
        public IEnumerable<Core.Models.Provider.Provider> UkrlpProviders { get; set; }
        public IEnumerable<int> ActiveProviders { get; set; }
        public IEnumerable<int> CourseDirectoryUkPrns { get; set; }
        public IEnumerable<FrameworkMetaData> Frameworks { get; set; }
        public IEnumerable<StandardMetaData> Standards { get; set; }
        public IEnumerable<AchievementRateProvider> AchievementRateProviders { get; set; }
        public IEnumerable<AchievementRateNational> AchievementRateNationals { get; set; }
        public IEnumerable<SatisfactionRateProvider> LearnerSatisfactionRates { get; set; }
        public IEnumerable<SatisfactionRateProvider> EmployerSatisfactionRates { get; set; }
        public IEnumerable<string> HeiProviders { get; set; }
    }
}