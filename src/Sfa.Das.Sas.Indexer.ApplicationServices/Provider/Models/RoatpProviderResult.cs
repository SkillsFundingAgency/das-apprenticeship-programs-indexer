using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    using System;

    public class RoatpProviderResult
    {
        public string Ukprn { get; set; }

        public string OrganisationName { get; set; }

        public ProviderType ProviderType { get; set; }

        public bool ContractedForNonLeviedEmployers { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

		public bool NotStartingNewApprentices { get; set; }

	    public bool IsDateValid()
        {
            if (StartDate == null)
            {
                return false;
            }

            if (StartDate?.Date <= DateTime.Today.Date && DateTime.Today.Date <= EndDate)
            {
                return true;
            }

            return StartDate?.Date <= DateTime.Today && EndDate == null;
        }
    }
}