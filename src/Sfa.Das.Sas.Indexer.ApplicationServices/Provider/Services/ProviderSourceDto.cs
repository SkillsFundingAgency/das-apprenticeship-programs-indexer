using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class ProviderSourceDto
    {
        public EmployerProviderResult EmployerProviders { get; set; }
        public CourseDirectoryResult CourseDirectoryProviders { get; set; }
        public UkrlpProviderResponse UkrlpProviders { get; set; }
        public FcsProviderResult ActiveProviders { get; set; }
        public List<RoatpProviderResult> RoatpProviders { get; set; }
        public IEnumerable<int> CourseDirectoryUkPrns => CourseDirectoryProviders.Providers.Select(x => x.Ukprn);
        public FrameworkMetaDataResult Frameworks { get; set; }
        public StandardMetaDataResult Standards { get; set; }
        public AchievementRateProviderResult AchievementRateProviders { get; set; }
        public AchievementRateNationalResult AchievementRateNationals { get; set; }
        public LearnerSatisfactionRateResult LearnerSatisfactionRates { get; set; }
        public EmployerSatisfactionRateResult EmployerSatisfactionRates { get; set; }
        public HeiProvidersResult HeiProviders { get; set; }
        public UkrlpProviderResponse UkrlpProvidersApi { get; set; }
    }
}