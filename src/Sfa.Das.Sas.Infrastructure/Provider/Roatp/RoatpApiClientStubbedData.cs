using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Roatp
{
    internal class RoatpApiClientStubbedData : IRoatpApiClient
    {
        public async Task<List<RoatpResult>> GetRoatpSummary()
        {
            var roatpSummaries = new List<RoatpResult>();

            roatpSummaries.Add(
                new RoatpResult
                {
                    ApplicationDeterminedDate = DateTime.Today.AddDays(10),
                    ContractedToDeliverToNonLeviedEmployers = "N",
                    EndDate = null,
                    NewOrganisationWithoutFinancialTrackRecord = "N",
                    OrganisationName = "Test Company 1",
                    ParentCompanyGuarantee = "N",
                    ProviderNotCurrentlyStartingNewApprentices = null,
                    ProviderType = "Supporting provider",
                    StartDate = new DateTime(2018,04,19),
                    Ukprn = "10030984"
                });

            roatpSummaries.Add(
                new RoatpResult
                {
                    ApplicationDeterminedDate = null,
                    ContractedToDeliverToNonLeviedEmployers = "Y",
                    EndDate = new DateTime(2050,09,04),
                    NewOrganisationWithoutFinancialTrackRecord = "Y",
                    OrganisationName = "Test Company 2",
                    ParentCompanyGuarantee = "N",
                    ProviderNotCurrentlyStartingNewApprentices = null,
                    ProviderType = "Main provider",
                    StartDate = new DateTime(2018, 08, 28),
                    Ukprn = "10003429"
                });

            roatpSummaries.Add(
                new RoatpResult
                {
                    ApplicationDeterminedDate = null,
                    ContractedToDeliverToNonLeviedEmployers = "N",
                    EndDate = null,
                    NewOrganisationWithoutFinancialTrackRecord = "Y",
                    OrganisationName = "Test Company 3",
                    ParentCompanyGuarantee = "N",
                    ProviderNotCurrentlyStartingNewApprentices = null,
                    ProviderType = "Employer provider",
                    StartDate = new DateTime(2018, 01, 30),
                    Ukprn = "10061818"
                });

            return roatpSummaries;
        }
    }
}
