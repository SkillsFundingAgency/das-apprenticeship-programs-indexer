using System;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback
{
    public class EmployerFeedbackSourceDto
    {
        public Guid Id { get; set; }

        public long Ukprn { get; set; }

        public long AccountId { get; set; }

        public Guid UserRef { get; set; }

        public DateTime DateTimeCompleted { get; set; }

        public List<ProviderAttributeSourceDto> ProviderAttributes { get; set; }

        public string ProviderRating { get; set; }
    }
}
