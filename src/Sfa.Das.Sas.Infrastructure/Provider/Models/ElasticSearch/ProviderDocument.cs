using Nest;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public class ProviderDocument
    {
        public ProviderDocument(string documentType)
        {
            DocumentType = documentType;
        }

        [Keyword]
        public string DocumentType { get; }

        // API/Programme shared properties
        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public string ProviderName { get; set; }

        public bool NationalProvider { get; set; }

        [Keyword(NullValue = "null")]
        public string Phone { get; set; }

        [Keyword(NullValue = "null")]
        public string Email { get; set; }

        public double? LearnerSatisfaction { get; set; }

        public double? EmployerSatisfaction { get; set; }

        [Keyword(NullValue = "null")]
        public string Website { get; set; }

        public bool HasParentCompanyGuarantee { get; set; }

        public bool IsNew { get; set; }

        public bool IsLevyPayerOnly { get; set; }

        public bool CurrentlyNotStartingNewApprentices { get; set; }

        public Feedback ProviderFeedback { get; set; }


        // Programme only shared properties
        public Guid Id { get; set; }

        public string LegalName { get; set; }

        public string ProviderMarketingInfo { get; set; }

        public string ApprenticeshipMarketingInfo { get; set; }

        [Keyword(NullValue = "null")]
        public string ContactUsUrl { get; set; }

        [Keyword(NullValue = "null")]
        public string ApprenticeshipInfoUrl { get; set; }

        public string[] DeliveryModes { get; set; }


        [Nested]
        public IEnumerable<TrainingLocation> TrainingLocations { get; set; }

        [GeoPoint]
        public IEnumerable<GeoCoordinate> LocationPoints { get; set; }

        public double? OverallAchievementRate { get; set; }

        public double? NationalOverallAchievementRate { get; set; }

        public string OverallCohort { get; set; }

        [Keyword(NullValue = "null")]
        public string[] DeliveryModesKeywords => DeliveryModes;

        public bool HasNonLevyContract { get; set; }


        // API Document only
        public string Uri { get; set; }

        public IEnumerable<string> Aliases { get; set; }

        public IEnumerable<ContactAddress> Addresses { get; set; }

        public bool? IsEmployerProvider { get; set; }

        public string MarketingInfo { get; set; }


        // Frameworks only
        public int? FrameworkCode { get; set; }

        public int? PathwayCode { get; set; }

        [Keyword(NullValue = "null")]
        public string FrameworkId { get; set; }

        public int? Level { get; set; }


        // Standards only
        public int? StandardCode { get; set; }

        public bool? RegulatedStandard { get; set; }
    }
}
