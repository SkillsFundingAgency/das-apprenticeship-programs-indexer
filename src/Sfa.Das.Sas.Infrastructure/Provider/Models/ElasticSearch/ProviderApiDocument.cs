using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public class ProviderApiDocument
    {
        public string Uri { get; set; }

        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public string ProviderName { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public IEnumerable<string> Aliases { get; set; }

        public IEnumerable<ContactAddress> Addresses { get; set; }

        public bool IsEmployerProvider { get; set; }

        [Keyword(NullValue = "null")]
        public string Phone { get; set; }

        [Keyword(NullValue = "null")]
        public string Email { get; set; }

        public bool NationalProvider { get; set; }

        [Keyword(NullValue = "null")]
        public string Website { get; set; }

        public double? EmployerSatisfaction { get; set; }

        public double? LearnerSatisfaction { get; set; }
        public string MarketingInfo { get; set; }
        public bool HasParentCompanyGuarantee { get; set; }
        public bool IsNew { get; set; }
        public bool IsLevyPayerOnly { get; set; }
        public bool CurrentlyNotStartingNewApprentices { get; set; }
        public Feedback ProviderFeedback { get; set; }
    }
}