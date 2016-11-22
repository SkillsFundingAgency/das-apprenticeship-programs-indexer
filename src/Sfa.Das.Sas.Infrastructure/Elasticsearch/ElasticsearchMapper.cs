﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Utility;
using Sfa.Das.Sas.Indexer.Core.Exceptions;
using Sfa.Das.Sas.Indexer.Core.Extensions;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

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
            var doc = new StandardDocument
            {
                StandardId = standard.Id,
                Published = standard.Published,
                Title = standard.Title,
                JobRoles = standard.JobRoles,
                Keywords = standard.Keywords,
                Level = standard.NotionalEndLevel,
                StandardPdf = standard.StandardPdfUrl,
                AssessmentPlanPdf = standard.AssessmentPlanPdfUrl,
                TypicalLength = standard.TypicalLength,
                OverviewOfRole = standard.OverviewOfRole,
                EntryRequirements = standard.EntryRequirements,
                WhatApprenticesWillLearn = standard.WhatApprenticesWillLearn,
                Qualifications = standard.Qualifications,
                ProfessionalRegistration = standard.ProfessionalRegistration,
                SectorSubjectAreaTier1 = standard.SectorSubjectAreaTier1,
                SectorSubjectAreaTier2 = standard.SectorSubjectAreaTier2
            };

            return doc;
        }

        public FrameworkDocument CreateFrameworkDocument(FrameworkMetaData frameworkMetaData)
        {
            // Trim off any whitespaces in the title or the Pathway Name
            frameworkMetaData.NasTitle = frameworkMetaData.NasTitle?.Trim();
            frameworkMetaData.PathwayName = frameworkMetaData.PathwayName?.Trim();

            var doc = new FrameworkDocument
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
            return doc;
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
                IsEmployerProvider = provider.IsEmployerProvider,
                Website = provider.ContactDetails.Website,
                Phone = provider.ContactDetails.Phone,
                Email = provider.ContactDetails.Email,
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
            IProviderAppreticeshipDocument documentToPopulate,
            Provider provider,
            IApprenticeshipInformation apprenticeshipInformation,
            List<DeliveryInformation> deliveryLocations)
        {
            var locations = GetTrainingLocations(deliveryLocations);
            var firstLoc = deliveryLocations.FirstOrDefault();

            documentToPopulate.Ukprn = provider.Ukprn;
            documentToPopulate.IsHigherEducationInstitute = provider.IsHigherEducationInstitute;
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
            documentToPopulate.IsEmployerProvider = provider.IsEmployerProvider;
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
