namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.CourseDirectory
{
    using System.Collections.Generic;
    using System.Linq;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using CoreProvider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;
    using CourseDirectoryProvider = Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory.Provider;
    using Location = Sfa.Das.Sas.Indexer.Core.Models.Provider.Location;

    public sealed class CourseDirectoryProviderMapper : ICourseDirectoryProviderMapper
    {
        private readonly ILog _logger;

        public CourseDirectoryProviderMapper(ILog logger)
        {
            _logger = logger;
        }

        public CoreProvider Map(CourseDirectoryProvider input)
        {
            var providerLocations = input.Locations.Select(MapToLocationEntity).ToList();

            var providerImport = new CoreProvider
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
                var matchingLocation = providerLocations.SingleOrDefault(x => x.Id == apprenticeshipLocation.ID);

                if (matchingLocation != default(Location))
                {
                    deliveryLocations.Add(
                    new DeliveryInformation { DeliveryLocation = matchingLocation, DeliveryModes = MapToDeliveryModes(apprenticeshipLocation.DeliveryModes), Radius = apprenticeshipLocation.Radius });
                }
                else
                {
                    _logger.Warn($"No matching location between provider and standard at standardLocation {apprenticeshipLocation.ID}");
                }
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

        private Location MapToLocationEntity(ApplicationServices.Provider.Models.CourseDirectory.Location matchingLocation)
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