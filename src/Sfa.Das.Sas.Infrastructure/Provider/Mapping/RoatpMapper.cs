using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Mapping
{
    public class RoatpMapper: IRoatpMapper
    {
        public List<RoatpProviderResult> Map(List<RoatpResult> roatpResults)
        {
            return roatpResults.Select(MapSingleRecord).ToList();
        }

        private static RoatpProviderResult MapSingleRecord(RoatpResult roatpResult)
        {

            ProviderType providerType;
            switch (roatpResult.ProviderType)
            {
                case "Main provider":
                    providerType = ProviderType.MainProvider;
                    break;
                case "Employer provider":
                    providerType = ProviderType.EmployerProvider;
                    break;
                case "Supporting provider":
                    providerType = ProviderType.SupportingProvider;
                    break;
                default:
                    providerType = ProviderType.Unknown;
                    break;
            }

           return new RoatpProviderResult
           {
               Ukprn = roatpResult.Ukprn,
               OrganisationName = roatpResult.OrganisationName,
               ProviderType = providerType,
               ContractedForNonLeviedEmployers = roatpResult.ContractedToDeliverToNonLeviedEmployers != null && roatpResult.ContractedToDeliverToNonLeviedEmployers.ToUpper() == "Y",
               ParentCompanyGuarantee = roatpResult.ParentCompanyGuarantee != null && roatpResult.ParentCompanyGuarantee.ToUpper() == "Y",
               NewOrganisationWithoutFinancialTrackRecord = roatpResult.NewOrganisationWithoutFinancialTrackRecord != null & roatpResult.NewOrganisationWithoutFinancialTrackRecord.ToUpper() == "Y",
               StartDate = roatpResult.StartDate,
               EndDate = roatpResult.EndDate,
               NotStartingNewApprentices = roatpResult.ProviderNotCurrentlyStartingNewApprentices != null
           };
        }
    }
}
