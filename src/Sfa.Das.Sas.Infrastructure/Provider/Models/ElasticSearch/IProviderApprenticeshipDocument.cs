using System;
using System.Collections.Generic;
using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public interface IProviderApprenticeshipDocument
    {
        Guid Id { get; set; }
        int Ukprn { get; set; }
        bool IsHigherEducationInstitute { get; set; }
        string ProviderName { get; set; }
        string LegalName { get; set; }
        bool NationalProvider { get; set; }
        string ProviderMarketingInfo { get; set; }
        string ApprenticeshipMarketingInfo { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string ContactUsUrl { get; set; }
        string ApprenticeshipInfoUrl { get; set; }
        double? LearnerSatisfaction { get; set; }
        double? EmployerSatisfaction { get; set; }
        string[] DeliveryModes { get; set; }
        string[] DeliveryModesKeywords { get; }
        string Website { get; set; }
        IEnumerable<TrainingLocation> TrainingLocations { get; set; }
        IEnumerable<GeoCoordinate> LocationPoints { get; set; }
        double? OverallAchievementRate { get; set; }
        double? NationalOverallAchievementRate { get; set; }
        string OverallCohort { get; set; }
        bool HasNonLevyContract { get; set; }
        bool HasParentCompanyGuarantee { get; set; }
        bool IsNew { get; set; }
        bool IsLevyPayerOnly { get; set; }
	    bool CurrentlyNotStartingNewApprentices { get; set; }
    }
}
