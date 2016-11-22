using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using CourseDirectoryProvider = Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory.Provider;
using Location = Sfa.Das.Sas.Indexer.Core.Models.Provider.Location;
using Provider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public sealed class CourseDirectoryProviderMapper : ICourseDirectoryProviderMapper
    {
        private readonly ILog _logger;

        public CourseDirectoryProviderMapper(ILog logger)
        {
            _logger = logger;
        }

        public Provider Map(CourseDirectoryProvider input)
        {
            var providerLocations = input.Locations.Select(MapToLocationEntity);

            var providerImport = new Provider
            {
                Id = input.Id.ToString(),
                Ukprn = input.Ukprn,
                Name = input.Name,
                NationalProvider = input.NationalProvider,
                ContactDetails =
                    new ContactInformation { Email = input.Email, Phone = input.Phone, Website = input.Website },
                EmployerSatisfaction = input.EmployerSatisfaction,
                LearnerSatisfaction = input.LearnerSatisfaction,
                MarketingInfo = input.MarketingInfo,
                Standards = GetStandardsFromIList(input.Standards, providerLocations),
                Frameworks = GetFrameworksFromIList(input.Frameworks, providerLocations),
                Locations = providerLocations
            };

            return providerImport;
        }

        public Provider PopulateExistingProviderFromCD(CourseDirectoryProvider input, Provider prov)
        {
            var providerLocations = input.Locations.Select(MapToLocationEntity);

            prov.Id = input.Id.ToString();

            prov.Name = !string.IsNullOrEmpty(input.Name) ? input.Name : prov.LegalName;

            prov.NationalProvider = input.NationalProvider;
            prov.ContactDetails =
                new ContactInformation {Email = input.Email, Phone = input.Phone, Website = input.Website};
            prov.EmployerSatisfaction = input.EmployerSatisfaction;
            prov.LearnerSatisfaction = input.LearnerSatisfaction;
            prov.MarketingInfo = input.MarketingInfo;
            prov.Standards = GetStandardsFromIList(input.Standards, providerLocations);
            prov.Frameworks = GetFrameworksFromIList(input.Frameworks, providerLocations);
            prov.Locations = providerLocations;

            return prov;
        }

        private IEnumerable<StandardInformation> GetStandardsFromIList(IList<Standard> standards, IEnumerable<Location> providerLocations)
        {
            return
                standards.Select(
                    standard =>
                        new StandardInformation
                        {
                            Code = standard.StandardCode,
                            InfoUrl = standard.StandardInfoUrl,
                            ContactInformation = new ContactInformation { Email = standard.Contact.Email, Phone = standard.Contact.Phone, Website = standard.Contact.ContactUsUrl },
                            MarketingInfo = standard.MarketingInfo,
                            DeliveryLocations = GetDeliveryLocations(standard.Locations, providerLocations)
                        }).ToList();
        }

        private IEnumerable<FrameworkInformation> GetFrameworksFromIList(IList<Framework> frameworks, IEnumerable<Das.Sas.Indexer.Core.Models.Provider.Location> providerLocations)
        {
            return
                frameworks.Select(
                    framework =>
                        new FrameworkInformation
                        {
                            Code = framework.FrameworkCode,
                            ProgType = framework.GetProgType,
                            PathwayCode = framework.PathwayCode,
                            InfoUrl = framework.FrameworkInfoUrl,
                            ContactInformation = new ContactInformation { Email = framework.Contact.Email, Phone = framework.Contact.Phone, Website = framework.Contact.ContactUsUrl },
                            MarketingInfo = framework.MarketingInfo,
                            DeliveryLocations = GetDeliveryLocations(framework.Locations, providerLocations)
                        }).ToList();
        }

        private IEnumerable<DeliveryInformation> GetDeliveryLocations(IList<LocationRef> apprenticshipLocations, IEnumerable<Location> providerLocations)
        {
            var deliveryLocations = new List<DeliveryInformation>(apprenticshipLocations.Count);

            foreach (var apprenticeshipLocation in apprenticshipLocations)
            {
                var matchingLocation = providerLocations.Single(x => x.Id == apprenticeshipLocation.ID);

                deliveryLocations.Add(
                    new DeliveryInformation { DeliveryLocation = matchingLocation, DeliveryModes = MapToDeliveryModes(apprenticeshipLocation.DeliveryModes), Radius = apprenticeshipLocation.Radius });
            }

            return deliveryLocations;
        }

        private IEnumerable<ModesOfDelivery> MapToDeliveryModes(IList<string> deliveryModes)
        {
            var modes = new List<ModesOfDelivery>(deliveryModes?.Count ?? 0);

            if (deliveryModes != null)
            {
                foreach (var mode in deliveryModes)
                {
                    switch (mode)
                    {
                        case "100PercentEmployer":
                            modes.Add(ModesOfDelivery.OneHundredPercentEmployer);
                            break;
                        case "BlockRelease":
                            modes.Add(ModesOfDelivery.BlockRelease);
                            break;
                        case "DayRelease":
                            modes.Add(ModesOfDelivery.DayRelease);
                            break;
                        default:
                            break;
                    }
                }
            }

            return modes;
        }

        private Coordinate GetGeoPoint(ApplicationServices.Provider.Models.CourseDirectory.Location matchingLocation)
        {
            if (!matchingLocation.Address.Latitude.HasValue || !matchingLocation.Address.Longitude.HasValue)
            {
                if (matchingLocation.ID.HasValue)
                {
                    _logger.Warn($"Location {matchingLocation.ID.Value} missing coordinates");
                }

                return null;
            }

            return new Coordinate { Latitude = matchingLocation.Address.Latitude.Value, Longitude = matchingLocation.Address.Longitude.Value };
        }

        private Core.Models.Provider.Location MapToLocationEntity(ApplicationServices.Provider.Models.CourseDirectory.Location matchingLocation)
        {
            var geopoint = GetGeoPoint(matchingLocation);

            return new Location
            {
                Id = matchingLocation.ID.Value,
                Name = matchingLocation.Name,
                Contact =
                    new ContactInformation { Email = matchingLocation.Email, Phone = matchingLocation.Phone, Website = matchingLocation.Website },
                Address =
                    new Core.Models.Provider.Address
                    {
                        Address1 = matchingLocation.Address.Address1,
                        Address2 = matchingLocation.Address.Address2,
                        Town = matchingLocation.Address.Town,
                        County = matchingLocation.Address.County,
                        Postcode = matchingLocation.Address.Postcode,
                        GeoPoint = geopoint
                    }
            };
        }
    }
}