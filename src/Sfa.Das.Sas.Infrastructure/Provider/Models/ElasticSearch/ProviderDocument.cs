using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models
{
    public class ProviderDocument
    {
        public string Uri { get; set; }

        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public string ProviderName { get; set; }

        public string LegalName { get; set; }

        public IEnumerable<ContactAddress> Addresses { get; set; }

        public bool IsEmployerProvider { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool NationalProvider { get; set; }

        public string Website { get; set; }

        public double? EmployerSatisfaction { get; set; }

        public double? LearnerSatisfaction { get; set; }
    }
}