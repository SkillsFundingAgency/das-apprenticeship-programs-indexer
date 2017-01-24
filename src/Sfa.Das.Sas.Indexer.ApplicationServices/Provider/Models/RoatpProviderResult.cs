namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    using System;

    public class RoatpProviderResult
    {
        public string Ukprn { get; set; }

        public string OrganisationName { get; set; }

        public string ProviderType { get; set; }

        public bool ContractedForNonLeviedEmployers { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}