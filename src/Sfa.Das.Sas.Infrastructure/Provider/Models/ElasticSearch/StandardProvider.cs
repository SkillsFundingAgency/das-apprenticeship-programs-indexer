using System.Collections.Generic;
using Nest;
using Newtonsoft.Json;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public sealed class StandardProvider : IProviderApprenticeshipDocument
    {
        public int StandardCode { get; set; }

        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public string ProviderName { get; set; }

        public string LegalName { get; set; }

        public bool NationalProvider { get; set; }

        public string ProviderMarketingInfo { get; set; }

        public string ApprenticeshipMarketingInfo { get; set; }

        public string Phone { get; set; }

        [Keyword(NullValue = "null")]
        public string Email { get; set; }

        [Keyword(NullValue = "null")]
        public string ContactUsUrl { get; set; }

        [Keyword(NullValue = "null")]
        public string ApprenticeshipInfoUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public double? LearnerSatisfaction { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public double? EmployerSatisfaction { get; set; }

        public string[] DeliveryModes { get; set; }

        public string Website { get; set; }

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

        public bool HasParentCompanyGuarantee { get; set; }

        public bool IsNew { get; set; }

        public bool IsLevyPayerOnly { get; set; }
    }
}