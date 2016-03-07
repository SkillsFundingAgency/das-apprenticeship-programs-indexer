﻿using System.Linq;

namespace Sfa.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Sfa.Eds.Das.Indexer.Core.Models.ProviderImport;
    using Sfa.Eds.Das.Indexer.Core.Services;
    using Sfa.Infrastructure.Models;
    using Sfa.Infrastructure.Settings;

    public class ProviderImportService : IImportProviders
    {
        private readonly ICourseDirectoryProviderDataService _courseDirectoryProvider;

        private readonly IInfrastructureSettings _settings;

        public ProviderImportService(IInfrastructureSettings settings)
        {
            _courseDirectoryProvider = new CourseDirectoryProviderDataService();
            _settings = settings;
        }

        public async Task<IEnumerable<ProviderImport>> GetProviders()
        {
            _courseDirectoryProvider.BaseUri = new Uri(_settings.CourseDirectoryUri);
            var responseAsync = _courseDirectoryProvider.BulkprovidersWithOperationResponseAsync();

            var providers = responseAsync.Result.Body;

            return providers.Select(provider => MapFromProviderToProviderImport(provider)).ToList();
        }

        public IEnumerable<string> GetDeliveryModesFromIList(IList<string> deliveryModes)
        {
            return deliveryModes.ToList();
        }

        public IEnumerable<Eds.Das.Indexer.Core.Models.ProviderImport.LocationRef> GetLocationRefFromIList(IList<Models.LocationRef> locations)
        {
            List<Eds.Das.Indexer.Core.Models.ProviderImport.LocationRef> result = new List<Eds.Das.Indexer.Core.Models.ProviderImport.LocationRef>();
            foreach (var location in locations)
            {
                var locRef = new Eds.Das.Indexer.Core.Models.ProviderImport.LocationRef
                {
                    ID = location.ID,
                    MarketingInfo = location.MarketingInfo,
                    Radius = int.Parse(location.Radius.ToString()),
                    StandardInfoUrl = location.StandardInfoUrl,
                    DeliveryModes = GetDeliveryModesFromIList(location.DeliveryModes)
                };
                result.Add(locRef);
            }

            return result;
        }

        public IEnumerable<Eds.Das.Indexer.Core.Models.ProviderImport.Standard> GetStandardsFromIList(IList<Models.Standard> standards)
        {
            List<Eds.Das.Indexer.Core.Models.ProviderImport.Standard> result = new List<Eds.Das.Indexer.Core.Models.ProviderImport.Standard>();
            foreach (var standard in standards)
            {
                var stan = new Eds.Das.Indexer.Core.Models.ProviderImport.Standard
                {
                    Contact = new Eds.Das.Indexer.Core.Models.ProviderImport.Contact
                    {
                        Email = standard.Contact.Email,
                        Phone = standard.Contact.Phone,
                        ContactUsUrl = standard.Contact.ContactUsUrl
                    },
                    MarketingInfo = standard.MarketingInfo,
                    StandardCode = standard.StandardCode,
                    StandardInfoUrl = standard.StandardInfoUrl,
                    Locations = GetLocationRefFromIList(standard.Locations)
                };
                result.Add(stan);
            }

            return result;
        }

        public IEnumerable<Eds.Das.Indexer.Core.Models.ProviderImport.Framework> GetFrameworksFromIList(IList<Models.Framework> frameworks)
        {
            List<Eds.Das.Indexer.Core.Models.ProviderImport.Framework> result = new List<Eds.Das.Indexer.Core.Models.ProviderImport.Framework>();
            foreach (var framework in frameworks)
            {
                var framew = new Eds.Das.Indexer.Core.Models.ProviderImport.Framework
                {
                    Contact = new Eds.Das.Indexer.Core.Models.ProviderImport.Contact
                    {
                        Email = framework.Contact.Email,
                        Phone = framework.Contact.Phone,
                        ContactUsUrl = framework.Contact.ContactUsUrl
                    },
                    Level = framework.Level,
                    FrameworkCode = framework.FrameworkCode,
                    PathwayCode = framework.PathwayCode,
                    Locations = GetLocationRefFromIList(framework.Locations)
                };
                result.Add(framew);
            }

            return result;
        }

        public IEnumerable<Eds.Das.Indexer.Core.Models.ProviderImport.Location> GetLocationFromIList(IList<Models.Location> locations)
        {
            List<Eds.Das.Indexer.Core.Models.ProviderImport.Location> result = new List<Eds.Das.Indexer.Core.Models.ProviderImport.Location>();

            foreach (var location in locations)
            {
                var loc = new Eds.Das.Indexer.Core.Models.ProviderImport.Location
                {
                    ID = location.ID,
                    Name = location.Name,
                    Address = new Eds.Das.Indexer.Core.Models.ProviderImport.Address
                    {
                        Phone = location.Address.Phone,
                        Email = location.Address.Email,
                        Address1 = location.Address.Address1,
                        Address2 = location.Address.Address2,
                        Country = location.Address.Country,
                        Postcode = location.Address.Postcode,
                        Town = location.Address.Postcode,
                        Website = location.Address.Website,
                        Lat = location.Address.Lat,
                        Long = location.Address.Long
                    }
                };
                result.Add(loc);
            }

            return result;
        }

        public ProviderImport MapFromProviderToProviderImport(Provider provider)
        {
            var providerImport = new ProviderImport
            {
                Email = provider.Email,
                EmployerSatisfaction = provider.EmployerSatisfaction,
                LearnerSatisfaction = provider.LearnerSatisfaction,
                MarketingInfo = provider.MarketingInfo,
                Name = provider.Name,
                Phone = provider.Phone,
                Ukprn = provider.Ukprn,
                Website = provider.Website,
                Standards = GetStandardsFromIList(provider.Standards),
                Frameworks = GetFrameworksFromIList(provider.Frameworks),
                Locations = GetLocationFromIList(provider.Locations)
            };

            return providerImport;
        }


        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IEnumerable<Provider>, IEnumerable<ProviderImport>>();
                cfg.CreateMap<Eds.Das.Indexer.Core.Models.ProviderImport.Address, Models.Address>();
                cfg.CreateMap<Eds.Das.Indexer.Core.Models.ProviderImport.Contact, Models.Contact>();
            });
            return config.CreateMapper();
        }
    }
}