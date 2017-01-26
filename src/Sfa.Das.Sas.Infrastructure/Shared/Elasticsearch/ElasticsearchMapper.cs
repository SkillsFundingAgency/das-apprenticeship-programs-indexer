using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Utility;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Core.Exceptions;
using Sfa.Das.Sas.Indexer.Core.Extensions;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using Address = Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models.Address;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using JobRoleItem = Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models.JobRoleItem;

    public class ElasticsearchMapper : IElasticsearchMapper
    {
        private readonly ILog _logger;
        private readonly IInfrastructureSettings _settings;

        public ElasticsearchMapper(ILog logger, IInfrastructureSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public StandardDocument CreateStandardDocument(StandardMetaData standard)
        {
            return new StandardDocument
            {
                StandardId = standard.Id,
                Published = standard.Published,
                Title = standard.Title,
                JobRoles = standard.JobRoles,
                Keywords = standard.Keywords,
                Level = standard.NotionalEndLevel,
                StandardPdf = standard.StandardPdfUrl,
                AssessmentPlanPdf = standard.AssessmentPlanPdfUrl,
                FundingCap = standard.FundingCap,
                Duration = standard.Duration,
                TypicalLength = standard.TypicalLength,
                OverviewOfRole = standard.OverviewOfRole,
                EntryRequirements = standard.EntryRequirements,
                WhatApprenticesWillLearn = standard.WhatApprenticesWillLearn,
                Qualifications = standard.Qualifications,
                ProfessionalRegistration = standard.ProfessionalRegistration,
                SectorSubjectAreaTier1 = standard.SectorSubjectAreaTier1,
                SectorSubjectAreaTier2 = standard.SectorSubjectAreaTier2
            };
        }

        public StandardLars CreateLarsStandardDocument(LarsStandard standard)
        {
            return new StandardLars
            {
                Id = standard.Id,
                Title = standard.Title,
                NotionalEndLevel = standard.NotionalEndLevel,
                StandardUrl = standard.StandardUrl,
                SectorSubjectAreaTier1 = standard.SectorSubjectAreaTier1,
                SectorSubjectAreaTier2 = standard.SectorSubjectAreaTier2,
                Duration = standard.Duration,
                FundingCap = standard.FundingCap
            };
        }

        public FrameworkDocument CreateFrameworkDocument(FrameworkMetaData frameworkMetaData)
        {
            // Trim off any whitespaces in the title or the Pathway Name
            frameworkMetaData.NasTitle = frameworkMetaData.NasTitle?.Trim();
            frameworkMetaData.PathwayName = frameworkMetaData.PathwayName?.Trim();

            return new FrameworkDocument
            {
                FrameworkId = string.Format(_settings.FrameworkIdFormat, frameworkMetaData.FworkCode, frameworkMetaData.ProgType, frameworkMetaData.PwayCode),
                Published = frameworkMetaData.Published,
                Title = CreateFrameworkTitle(frameworkMetaData.NasTitle, frameworkMetaData.PathwayName),
                FrameworkCode = frameworkMetaData.FworkCode,
                FrameworkName = frameworkMetaData.NasTitle,
                PathwayCode = frameworkMetaData.PwayCode,
                PathwayName = frameworkMetaData.PathwayName,
                ProgType = frameworkMetaData.ProgType,
                Level = MapToLevelFromProgType(frameworkMetaData.ProgType),
                JobRoleItems = frameworkMetaData.JobRoleItems?.Select(m => new JobRoleItem { Title = m.Title, Description = m.Description }),
                Keywords = frameworkMetaData.Keywords,
                FundingCap = frameworkMetaData.FundingCap,
                Duration = frameworkMetaData.Duration,
                TypicalLength = frameworkMetaData.TypicalLength,
                ExpiryDate = frameworkMetaData.EffectiveTo,
                SectorSubjectAreaTier1 = frameworkMetaData.SectorSubjectAreaTier1,
                SectorSubjectAreaTier2 = frameworkMetaData.SectorSubjectAreaTier2,
                CompletionQualifications = frameworkMetaData.CompletionQualifications,
                EntryRequirements = frameworkMetaData.EntryRequirements,
                ProfessionalRegistration = frameworkMetaData.ProfessionalRegistration,
                FrameworkOverview = frameworkMetaData.FrameworkOverview,
                CompetencyQualification = frameworkMetaData.CompetencyQualification,
                KnowledgeQualification = frameworkMetaData.KnowledgeQualification,
                CombinedQualification = frameworkMetaData.CombinedQualification
            };
        }

        public FrameworkLars CreateLarsFrameworkDocument(FrameworkMetaData frameworkMetaData)
        {
            // Trim off any whitespaces in the title or the Pathway Name
            frameworkMetaData.NasTitle = frameworkMetaData.NasTitle?.Trim();
            frameworkMetaData.PathwayName = frameworkMetaData.PathwayName?.Trim();

            return new FrameworkLars
            {
                CombinedQualification = frameworkMetaData.CombinedQualification,
                CompetencyQualification = frameworkMetaData.CompetencyQualification,
                CompletionQualifications = frameworkMetaData.CompletionQualifications,
                EffectiveFrom = frameworkMetaData.EffectiveFrom,
                EffectiveTo = frameworkMetaData.EffectiveTo,
                EntryRequirements = frameworkMetaData.EntryRequirements,
                FrameworkOverview = frameworkMetaData.FrameworkOverview,
                FworkCode = frameworkMetaData.FworkCode,
                JobRoleItems = frameworkMetaData.JobRoleItems,
                Keywords = frameworkMetaData.Keywords,
                KnowledgeQualification = frameworkMetaData.KnowledgeQualification,
                NasTitle = frameworkMetaData.NasTitle,
                PathwayName = frameworkMetaData.PathwayName,
                ProfessionalRegistration = frameworkMetaData.ProfessionalRegistration,
                ProgType = frameworkMetaData.ProgType,
                PwayCode = frameworkMetaData.PwayCode,
                SectorSubjectAreaTier1 = frameworkMetaData.SectorSubjectAreaTier1,
                SectorSubjectAreaTier2 = frameworkMetaData.SectorSubjectAreaTier1,
                Duration = frameworkMetaData.Duration,
                FundingCap = frameworkMetaData.FundingCap
            };
        }

        public FundingDocument CreateFundingMetaDataDocument(FundingMetaData fundingMetaData)
        {
            return new FundingDocument
            {
                EffectiveFrom = fundingMetaData.EffectiveFrom,
                EffectiveTo = fundingMetaData.EffectiveTo,
                FundingCategory = fundingMetaData.FundingCategory,
                LearnAimRef = fundingMetaData.LearnAimRef,
                RateWeighted = fundingMetaData.RateWeighted
            };
        }

        public FrameworkAimDocument CreateFrameworkAimMetaDataDocument(FrameworkAimMetaData frameworkAimMetaData)
        {
            return new FrameworkAimDocument
            {
                EffectiveFrom = frameworkAimMetaData.EffectiveFrom,
                EffectiveTo = frameworkAimMetaData.EffectiveTo,
                LearnAimRef = frameworkAimMetaData.LearnAimRef,
                FworkCode = frameworkAimMetaData.FworkCode,
                PwayCode = frameworkAimMetaData.PwayCode,
                ProgType = frameworkAimMetaData.ProgType,
                ApprenticeshipComponentType = frameworkAimMetaData.ApprenticeshipComponentType
            };
        }

        public LearningDeliveryDocument CreateLearningDeliveryMetaDataDocument(LearningDeliveryMetaData learningDeliveryMetaData)
        {
            return new LearningDeliveryDocument
            {
                EffectiveFrom = learningDeliveryMetaData.EffectiveFrom,
                EffectiveTo = learningDeliveryMetaData.EffectiveTo,
                LearnAimRef = learningDeliveryMetaData.LearnAimRef,
                LearnAimRefTitle = learningDeliveryMetaData.LearnAimRefTitle,
                LearnAimRefType = learningDeliveryMetaData.LearnAimRefType
            };
        }

        public ApprenticeshipFundingDocument CreateApprenticeshipFundingDocument(ApprenticeshipFundingMetaData apprenticeshipFunding)
        {
            return new ApprenticeshipFundingDocument
            {
                ProgType = apprenticeshipFunding.ProgType,
                ApprenticeshipCode = apprenticeshipFunding.ApprenticeshipCode,
                PwayCode = apprenticeshipFunding.PwayCode,
                ReservedValue1 = apprenticeshipFunding.ReservedValue1,
                ApprenticeshipType = apprenticeshipFunding.ApprenticeshipType,
                MaxEmployerLevyCap = apprenticeshipFunding.MaxEmployerLevyCap
            };
        }

        public ApprenticeshipComponentTypeDocument CreateApprenticeshipComponentTypeMetaDataDocument(ApprenticeshipComponentTypeMetaData apprenticeshipComponentTypeMetaData)
        {
            return new ApprenticeshipComponentTypeDocument
            {
                EffectiveTo = apprenticeshipComponentTypeMetaData.EffectiveTo,
                EffectiveFrom = apprenticeshipComponentTypeMetaData.EffectiveFrom,
                ApprenticeshipComponentType = apprenticeshipComponentTypeMetaData.ApprenticeshipComponentType,
                ApprenticeshipComponentTypeDesc = apprenticeshipComponentTypeMetaData.ApprenticeshipComponentTypeDesc,
                ApprenticeshipComponentTypeDesc2 = apprenticeshipComponentTypeMetaData.ApprenticeshipComponentTypeDesc2
           };
        }

        public OrganisationDocument CreateOrganisationDocument(Organisation organisation)
        {
            return new OrganisationDocument
            {
                EpaOrganisationIdentifier = organisation.EpaOrganisationIdentifier,
                OrganisationType = organisation.OrganisationType,
                Address = new Address
                {
                    Primary = organisation.Address.Primary,
                    Secondary = organisation.Address.Secondary,
                    Street = organisation.Address.Street,
                    Town = organisation.Address.Town,
                    Postcode = organisation.Address.Postcode,
                },
                EpaOrganisation = organisation.EpaOrganisation,
                WebsiteLink = organisation.WebsiteLink,
            };
        }

        public StandardOrganisationDocument CreateStandardOrganisationDocument(StandardOrganisationsData standardOrganisationsData)
        {
            return new StandardOrganisationDocument
            {
                EpaOrganisationIdentifier = standardOrganisationsData.EpaOrganisationIdentifier,
                StandardCode = standardOrganisationsData.StandardCode,
                EffectiveFrom = standardOrganisationsData.EffectiveFrom
            };
        }

        public int MapToLevelFromProgType(int progType)
        {
            return ApprenticeshipLevelMapper.MapToLevel(progType);
        }

        public StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, DeliveryInformation deliveryInformation)
        {
            return CreateStandardProviderDocument(provider, standardInformation, new List<DeliveryInformation> { deliveryInformation });
        }

        public StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, IEnumerable<DeliveryInformation> deliveryInformation)
        {
            return CreateStandardProviderDocument(provider, standardInformation, deliveryInformation.ToList());
        }

        public FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation frameworkInformation, DeliveryInformation deliveryInformation)
        {
            return CreateFrameworkProviderDocument(provider, frameworkInformation, new List<DeliveryInformation> { deliveryInformation });
        }

        public ProviderDocument CreateProviderDocument(Provider provider)
        {
            var providerDocument = new ProviderDocument
            {
                Ukprn = provider.Ukprn,
                IsHigherEducationInstitute = provider.IsHigherEducationInstitute,
                NationalProvider = provider.NationalProvider,
                ProviderName = provider.Name,
                LegalName = provider.LegalName,
                Aliases = provider.Aliases,
                Addresses = provider.Addresses,
                IsEmployerProvider = provider.IsEmployerProvider,
                Website = provider.ContactDetails?.Website,
                Phone = provider.ContactDetails?.Phone,
                Email = provider.ContactDetails?.Email,
                EmployerSatisfaction = provider.EmployerSatisfaction,
                LearnerSatisfaction = provider.LearnerSatisfaction
            };

            return providerDocument;
        }

        public ProviderApiDocument CreateProviderApiDocument(Provider provider)
        {
            var providerDocument = new ProviderApiDocument
            {
                Ukprn = provider.Ukprn,
                IsHigherEducationInstitute = provider.IsHigherEducationInstitute,
                NationalProvider = provider.NationalProvider,
                ProviderName = provider.Name,
                LegalName = provider.LegalName,
                Aliases = provider.Aliases,
                Addresses = provider.Addresses,
                IsEmployerProvider = provider.IsEmployerProvider,
                Website = provider.ContactDetails?.Website,
                Phone = provider.ContactDetails?.Phone,
                Email = provider.ContactDetails?.Email,
                EmployerSatisfaction = provider.EmployerSatisfaction,
                LearnerSatisfaction = provider.LearnerSatisfaction
            };

            return providerDocument;
        }

        public FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation frameworkInformation, IEnumerable<DeliveryInformation> deliveryInformation)
        {
            return CreateFrameworkProviderDocument(provider, frameworkInformation, deliveryInformation.ToList());
        }

        private StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, List<DeliveryInformation> deliveryInformation)
        {
            try
            {
                var standardProvider = new StandardProvider
                {
                    StandardCode = standardInformation.Code
                };

                PopulateDocumentSharedProperties(standardProvider, provider, standardInformation, deliveryInformation);

                return standardProvider;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {
                throw new MappingException("Unable to map to Standard Provider Document", ex);
            }
        }

        private FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation frameworkInformation, List<DeliveryInformation> deliveryInformation)
        {
            try
            {
                var frameworkProvider = new FrameworkProvider
                {
                    FrameworkCode = frameworkInformation.Code,
                    PathwayCode = frameworkInformation.PathwayCode,
                    FrameworkId = string.Format(_settings.FrameworkIdFormat, frameworkInformation.Code, frameworkInformation.ProgType, frameworkInformation.PathwayCode),
                    Level = MapToLevelFromProgType(frameworkInformation.ProgType)
                };

                PopulateDocumentSharedProperties(frameworkProvider, provider, frameworkInformation, deliveryInformation);

                return frameworkProvider;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {
                throw new MappingException("Unable to map to Framework Provider Document", ex);
            }
        }

        private void PopulateDocumentSharedProperties(
            IProviderApprenticeshipDocument documentToPopulate,
            Provider provider,
            IApprenticeshipInformation apprenticeshipInformation,
            List<DeliveryInformation> deliveryLocations)
        {
            var locations = GetTrainingLocations(deliveryLocations);
            var firstLoc = deliveryLocations.FirstOrDefault();

            documentToPopulate.Ukprn = provider.Ukprn;
            documentToPopulate.IsHigherEducationInstitute = provider.IsHigherEducationInstitute;
            documentToPopulate.HasNonLevyContract = provider.HasNonLevyContract;
            documentToPopulate.HasParentCompanyGuarantee = provider.HasParentCompanyGuarantee;
            documentToPopulate.IsNew = provider.IsNew;
            documentToPopulate.ProviderName = provider.Name;
            documentToPopulate.NationalProvider = provider.NationalProvider;
            documentToPopulate.ProviderMarketingInfo = EscapeSpecialCharacters(provider.MarketingInfo);
            documentToPopulate.ApprenticeshipMarketingInfo = EscapeSpecialCharacters(apprenticeshipInformation.MarketingInfo);
            documentToPopulate.Phone = apprenticeshipInformation.ContactInformation.Phone;
            documentToPopulate.Email = apprenticeshipInformation.ContactInformation.Email;
            documentToPopulate.ContactUsUrl = apprenticeshipInformation.ContactInformation.Website;
            documentToPopulate.ApprenticeshipInfoUrl = apprenticeshipInformation.InfoUrl;
            documentToPopulate.LearnerSatisfaction = provider.LearnerSatisfaction;
            documentToPopulate.EmployerSatisfaction = provider.EmployerSatisfaction;
            documentToPopulate.DeliveryModes = firstLoc == null ? new List<string>().ToArray() : GenerateListOfDeliveryModes(firstLoc.DeliveryModes);
            documentToPopulate.Website = firstLoc == null ? string.Empty : firstLoc.DeliveryLocation.Contact.Website;
            documentToPopulate.TrainingLocations = locations;
            documentToPopulate.LocationPoints = GetLocationPoints(deliveryLocations);

            documentToPopulate.OverallAchievementRate = GetRoundedValue(apprenticeshipInformation.OverallAchievementRate);
            documentToPopulate.NationalOverallAchievementRate = GetRoundedValue(apprenticeshipInformation.NationalOverallAchievementRate);
            documentToPopulate.OverallCohort = apprenticeshipInformation.OverallCohort;
        }

        private double? GetRoundedValue(double? value)
        {
            if (value != null)
            {
                return Math.Round((double)value);
            }

            return null;
        }

        private IEnumerable<GeoCoordinate> GetLocationPoints(IEnumerable<DeliveryInformation> deliveryLocations)
        {
            var points = new List<GeoCoordinate>();

            foreach (var location in deliveryLocations)
            {
                points.Add(new GeoCoordinate(
                                            location.DeliveryLocation.Address?.GeoPoint?.Latitude ?? 0,
                                            location.DeliveryLocation.Address?.GeoPoint?.Longitude ?? 0));
            }

            return points;
        }

        private List<TrainingLocation> GetTrainingLocations(IEnumerable<DeliveryInformation> deliveryLocations)
        {
            var locations = new List<TrainingLocation>();
            foreach (var loc in deliveryLocations)
            {
                locations.Add(
                    new TrainingLocation
                    {
                        LocationId = loc.DeliveryLocation.Id,
                        LocationName = loc.DeliveryLocation.Name,
                        Address =
                                new Models.Address
                                {
                                    Address1 = EscapeSpecialCharacters(loc.DeliveryLocation.Address.Address1),
                                    Address2 = EscapeSpecialCharacters(loc.DeliveryLocation.Address.Address2),
                                    Town = EscapeSpecialCharacters(loc.DeliveryLocation.Address.Town),
                                    County = EscapeSpecialCharacters(loc.DeliveryLocation.Address.County),
                                    PostCode = loc.DeliveryLocation.Address.Postcode,
                                },
                        Location =
                                new CircleGeoShape
                                {
                                    Coordinates =
                                            new GeoCoordinate(
                                            loc.DeliveryLocation.Address?.GeoPoint?.Latitude ?? 0,
                                            loc.DeliveryLocation.Address?.GeoPoint?.Longitude ?? 0),
                                    Radius = $"{loc.Radius}mi"
                                },
                        LocationPoint = new GeoCoordinate(
                                            loc.DeliveryLocation.Address?.GeoPoint?.Latitude ?? 0,
                                            loc.DeliveryLocation.Address?.GeoPoint?.Longitude ?? 0)
                    });
            }

            return locations;
        }

        private string[] GenerateListOfDeliveryModes(IEnumerable<ModesOfDelivery> deliveryModes)
        {
            return deliveryModes.Select(x => x.GetDescription()).ToArray();
        }

        private string EscapeSpecialCharacters(string marketingInfo)
        {
            if (marketingInfo == null)
            {
                return null;
            }

            return marketingInfo.Replace(Environment.NewLine, "\\r\\n").Replace("\n", "\\n").Replace("\"", "\\\"");
        }

        private string CreateFrameworkTitle(string framworkname, string pathwayName)
        {
            return $"{framworkname}: {pathwayName}";
        }
    }
}
