using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Core.Models.Provider
{
    public sealed class Provider : IIndexEntry
    {
        public string Id { get; set; }

        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public bool HasNonLevyContract { get; set; }

        public bool HasParentCompanyGuarantee { get; set; }

        public bool IsNew { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Aliases { get; set; }

        public string LegalName { get; set; }

        public bool NationalProvider { get; set; }

        public ContactInformation ContactDetails { get; set; }

        public string MarketingInfo { get; set; }

        public double? EmployerSatisfaction { get; set; }

        public double? LearnerSatisfaction { get; set; }

        public IEnumerable<FrameworkInformation> Frameworks { get; set; }

        public IEnumerable<Location> Locations { get; set; }

        public IEnumerable<StandardInformation> Standards { get; set; }

        public IEnumerable<ContactAddress> Addresses { get; set; }

        public bool IsEmployerProvider { get; set; }
    }
}