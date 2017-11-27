using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Core.Exceptions;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.Provider;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services;
using Address = Sfa.Das.Sas.Indexer.Core.Models.Provider.Address;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Elasticsearch
{
    [TestFixture]
    public class ElasticsearchMapperTest
    {
        private Mock<IInfrastructureSettings> _settings;
        private string _frameworkIdFormat = "{0}{1}{2}";
        private Mock<IOrganisationTypeProcessor> _organisationTypeProcessor;

        [OneTimeSetUp]
        public void SetupFixture()
        {
            _settings = new Mock<IInfrastructureSettings>();
            _settings.SetupGet(x => x.FrameworkIdFormat).Returns(_frameworkIdFormat);
            _organisationTypeProcessor = new Mock<IOrganisationTypeProcessor>();
            _organisationTypeProcessor.Setup(x => x.ProcessOrganisationType(It.IsAny<string>())).Returns<string>(x => x);
        }

        [Test]
        public void WhenCreatingFrameworkDocument()
        {
            var frameworkMetaData = new FrameworkMetaData
            {
                EffectiveFrom = DateTime.Parse("10-Feb-14"),
                EffectiveTo = DateTime.MinValue,
                FworkCode = 123,
                PwayCode = 1,
                NasTitle = "Sustainable Resource Operations and Management",
                PathwayName = "Higher Apprenticeship in Sustainable Resource Operations and Management",
                ProgType = 20,
                JobRoleItems = new List<JobRoleItem>
                    {
                        new JobRoleItem { Title = "Title 1", Description = "Description 1" }
                    },
                Keywords = new[] { "keyword1", "keyword2" },
                TypicalLength = new TypicalLength { From = 12, To = 24, Unit = "m" },
                Duration = 12
            };

            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());

            var framework = mapper.CreateFrameworkDocument(frameworkMetaData);

            framework.Title.Should().Be("Sustainable Resource Operations and Management: Higher Apprenticeship in Sustainable Resource Operations and Management");
            framework.Level.Should().Be(4);
            framework.FrameworkId.Should().Be(string.Format(_frameworkIdFormat, frameworkMetaData.FworkCode, frameworkMetaData.ProgType, frameworkMetaData.PwayCode));
            framework.JobRoleItems.Count().Should().Be(1);
            framework.Keywords.Should().Contain(new[] { "keyword1", "keyword2" });
            framework.TypicalLength.From.ShouldBeEquivalentTo(12);
            framework.TypicalLength.To.ShouldBeEquivalentTo(24);
            framework.Duration.Should().Be(12);
        }

        [Test]
        public void WhenCreatingOrganisationDocument()
        {
            const string epaOrganisationIdentifier = "OrgIdentifier";
            const string organisationType = "Awarding Organisation";
            const string email = "test@testemail.com";
            const string phone = "555-5555";
            const string epaOrganisation = "EpaOrg";
            const string websiteLink = "http://testemail.com";
            var address = new Indexer.Core.AssessmentOrgs.Models.Address
            {
                Primary = "primary address",
                Secondary = "secondary address",
                Street = "address street",
                Town = "address town",
                Postcode = "PO1 1OP"
            };

            var organisation = new Organisation
            {
                EpaOrganisationIdentifier = epaOrganisationIdentifier,
                OrganisationType = organisationType,
                Email = email,
                Phone = phone,
                Address = address,
                EpaOrganisation = epaOrganisation,
                WebsiteLink = websiteLink
            };

            var mapper = new ElasticsearchMapper(_settings.Object, _organisationTypeProcessor.Object);

            var organisationDocument = mapper.CreateOrganisationDocument(organisation);

            organisationDocument.EpaOrganisationIdentifier.Should().Be(epaOrganisationIdentifier);
            organisationDocument.OrganisationType.Should().Be(organisationType);
            organisationDocument.Email.Should().Be(email);
            organisationDocument.Phone.Should().Be(phone);
            organisationDocument.EpaOrganisation.Should().Be(epaOrganisation);
            organisationDocument.WebsiteLink.Should().Be(websiteLink);
            organisationDocument.Address.Primary.Should().Be(address.Primary);
            organisationDocument.Address.Secondary.Should().Be(address.Secondary);
            organisationDocument.Address.Street.Should().Be(address.Street);
            organisationDocument.Address.Town.Should().Be(address.Town);
            organisationDocument.Address.Postcode.Should().Be(address.Postcode);
        }

        [Test]
        public void WhenCreatingStandardOrganisationDocument()
        {
            const string epaOrganisationIdentifier = "OrgIdentifier";
            const string organisationType = "Awarding Organisation";
            const string email = "test@testemail.com";
            const string phone = "555-5555";
            const string epaOrganisation = "EpaOrg";
            const string websiteLink = "http://testemail.com";
            const string standardCode = "standard_code";
            var effectiveFrom = DateTime.Today.AddDays(-7);
            var effectiveTo = DateTime.Today.AddDays(7);

            var address = new Indexer.Core.AssessmentOrgs.Models.Address
            {
                Primary = "primary address",
                Secondary = "secondary address",
                Street = "address street",
                Town = "address town",
                Postcode = "PO1 1OP"
            };

            var standardOrganisationsData = new StandardOrganisationsData
            {
                EpaOrganisationIdentifier = epaOrganisationIdentifier,
                OrganisationType = organisationType,
                Email = email,
                Phone = phone,
                Address = address,
                EpaOrganisation = epaOrganisation,
                WebsiteLink = websiteLink,
                StandardCode = standardCode,
                EffectiveFrom = effectiveFrom,
                EffectiveTo = effectiveTo
            };

            var mapper = new ElasticsearchMapper(_settings.Object, _organisationTypeProcessor.Object);

            var standardOrganisationDocument = mapper.CreateStandardOrganisationDocument(standardOrganisationsData);

            standardOrganisationDocument.EpaOrganisationIdentifier.Should().Be(epaOrganisationIdentifier);
            standardOrganisationDocument.OrganisationType.Should().Be(organisationType);
            standardOrganisationDocument.Email.Should().Be(email);
            standardOrganisationDocument.Phone.Should().Be(phone);
            standardOrganisationDocument.EpaOrganisation.Should().Be(epaOrganisation);
            standardOrganisationDocument.WebsiteLink.Should().Be(websiteLink);
            standardOrganisationDocument.Address.Primary.Should().Be(address.Primary);
            standardOrganisationDocument.Address.Secondary.Should().Be(address.Secondary);
            standardOrganisationDocument.Address.Street.Should().Be(address.Street);
            standardOrganisationDocument.Address.Town.Should().Be(address.Town);
            standardOrganisationDocument.Address.Postcode.Should().Be(address.Postcode);
            standardOrganisationDocument.StandardCode.Should().Be(standardCode);
            standardOrganisationDocument.EffectiveFrom.Should().Be(effectiveFrom);
            standardOrganisationDocument.EffectiveTo.Should().Be(effectiveTo);
        }

        [Test]
        public void WhenCreatingFrameworkDocumentAndTitleIsPathway()
        {
            var frameworkMetaData = new FrameworkMetaData
            {
                EffectiveFrom = DateTime.Parse("10-Feb-14"),
                EffectiveTo = DateTime.MinValue,
                FworkCode = 616,
                PwayCode = 1,
                NasTitle = "Trade Business Services",
                PathwayName = "Trade Business Services",
                ProgType = 3
            };

            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());

            var framework = mapper.CreateFrameworkDocument(frameworkMetaData);

            framework.Title.Should().Be("Trade Business Services: Trade Business Services");
            framework.Level.Should().Be(2);
            framework.FrameworkId.Should().Be(string.Format(_frameworkIdFormat, frameworkMetaData.FworkCode, frameworkMetaData.ProgType, frameworkMetaData.PwayCode));
        }

        [Test]
        public void WhenCreatingFrameworkDocumentAndPathwaySameAsTitleButWithTrailingSpace()
        {
            var frameworkMetaData = new FrameworkMetaData
            {
                EffectiveFrom = DateTime.Parse("10-Feb-14"),
                EffectiveTo = DateTime.MinValue,
                FworkCode = 616,
                PwayCode = 1,
                NasTitle = "Accounting",
                PathwayName = "Accounting ",
                ProgType = 3
            };

            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());

            var framework = mapper.CreateFrameworkDocument(frameworkMetaData);

            framework.Title.Should().Be("Accounting: Accounting");
            framework.Level.Should().Be(2);
            framework.FrameworkId.Should().Be(string.Format(_frameworkIdFormat, frameworkMetaData.FworkCode, frameworkMetaData.ProgType, frameworkMetaData.PwayCode));
        }

        [Test]
        public void ShouldThrowMappingExceptionOnMappingErrorForFrameworkProviderMapping()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            // Remove Delivery modes
            testProvider.Frameworks.First().DeliveryLocations.First().DeliveryModes = null;

            Assert.Throws<MappingException>(() =>
                mapper.CreateFrameworkProviderDocument(
                    testProvider,
                    testProvider.Frameworks.First(),
                    testProvider.Frameworks.First().DeliveryLocations.First()));
        }

        [Test]
        public void ShouldThrowMappingExceptionOnMappingErrorForStandardProviderMapping()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            // Remove Delivery modes
            testProvider.Standards.First().DeliveryLocations.First().DeliveryModes = null;

            Assert.Throws<MappingException>(() =>
                mapper.CreateStandardProviderDocument(
                    testProvider,
                    testProvider.Standards.First(),
                    testProvider.Standards.First().DeliveryLocations.First()));
        }

        [Test]
        public void ShouldCreateFrameworkProviderDocumentWithListOfProviderLocations()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            var document = mapper.CreateFrameworkProviderDocument(testProvider, testProvider.Frameworks.First(), testProvider.Frameworks.First().DeliveryLocations);

            document.TrainingLocations.Count().Should().Be(1);
        }

        [Test]
        public void ShouldCreateFrameworkProviderDocumentWithListOfLocationPoints()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            var document = mapper.CreateFrameworkProviderDocument(testProvider, testProvider.Frameworks.First(), testProvider.Frameworks.First().DeliveryLocations);

            document.LocationPoints.Count().Should().Be(1);
        }

        [Test]
        public void ShouldCreateValidFrameworkProviderDocument()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            var document = mapper.CreateFrameworkProviderDocument(testProvider, testProvider.Frameworks.First(), testProvider.Frameworks.First().DeliveryLocations.First());

            document.FrameworkCode.Should().Be(99);
            document.PathwayCode.Should().Be(1122);
            document.FrameworkId.Should().Be(string.Format(_frameworkIdFormat, 99, 20, 1122));
            document.Level.Should().Be(4);

            document.Ukprn.Should().Be(4556);
            document.ProviderName.Should().Be("Test Provider");
            document.TrainingLocations.First().LocationId.Should().Be(77);
            document.TrainingLocations.First().LocationName.Should().Be("Framework Test Location");
            document.ProviderMarketingInfo.Should().Be("Provider Marketing");
            document.ApprenticeshipMarketingInfo.Should().Be("Framework Apprenticeship Marketing");
            document.Phone.Should().Be("12324-5678");
            document.Email.Should().Be("test@test.com");
            document.ContactUsUrl.Should().Be("http://contact-us.com");
            document.ApprenticeshipInfoUrl.Should().Be("http://standard-info.com");
            document.LearnerSatisfaction.Should().Be(8.2);
            document.EmployerSatisfaction.Should().Be(9.2);
            document.DeliveryModes.Should().BeEquivalentTo("BlockRelease", "DayRelease");
            document.Website.Should().Be("http://location-site");
            document.TrainingLocations.First().Address.Address1.Should().Be("Framework Test Address1");
            document.TrainingLocations.First().Address.Address2.Should().Be("Framework Test Address2");
            document.TrainingLocations.First().Address.Town.Should().Be("Framework Test Town");
            document.TrainingLocations.First().Address.County.Should().Be("Framework Test County");
            document.TrainingLocations.First().Address.PostCode.Should().Be("TE3 5ES");

            document.TrainingLocations.FirstOrDefault()?.LocationPoint.Latitude.Should().Be(53.213);
            document.TrainingLocations.FirstOrDefault()?.LocationPoint.Longitude.Should().Be(-50.123);
            document.TrainingLocations.FirstOrDefault()?.Location.Coordinates.Latitude.Should().Be(53.213);
            document.TrainingLocations.FirstOrDefault()?.Location.Coordinates.Longitude.Should().Be(-50.123);
            document.TrainingLocations.FirstOrDefault()?.Location.Radius.Should().Be("25mi");
        }

        [Test]
        public void ShouldCreateStandardProviderDocumentWithListOfProviderLocations()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            var document = mapper.CreateStandardProviderDocument(testProvider, testProvider.Standards.First(), testProvider.Frameworks.First().DeliveryLocations);

            document.TrainingLocations.Count().Should().Be(1);
        }

        [Test]
        public void ShouldCreateStandardProviderDocumentWithListLocationPoints()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            var document = mapper.CreateStandardProviderDocument(testProvider, testProvider.Standards.First(), testProvider.Frameworks.First().DeliveryLocations);

            document.LocationPoints.Count().Should().Be(1);
        }

        [Test]
        public void ShouldCreateValidStandardProviderDocument()
        {
            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());
            var testProvider = GenerateTestProvider();

            var document = mapper.CreateStandardProviderDocument(testProvider, testProvider.Standards.First(), testProvider.Standards.First().DeliveryLocations.First());

            document.StandardCode.Should().Be(101);
            document.Ukprn.Should().Be(4556);
            document.ProviderName.Should().Be("Test Provider");
            document.TrainingLocations.First().LocationId.Should().Be(98);
            document.TrainingLocations.First().LocationName.Should().Be("Standard Test Location");
            document.ProviderMarketingInfo.Should().Be("Provider Marketing");
            document.ApprenticeshipMarketingInfo.Should().Be("Standard Apprenticeship Marketing");
            document.Phone.Should().Be("5555-5678");
            document.Email.Should().Be("test@test.com");
            document.ContactUsUrl.Should().Be("http://contact-us.com");
            document.ApprenticeshipInfoUrl.Should().Be("http://standard-info.com");
            document.LearnerSatisfaction.Should().Be(8.2);
            document.EmployerSatisfaction.Should().Be(9.2);
            document.DeliveryModes.Should().BeEquivalentTo("BlockRelease", "DayRelease");
            document.Website.Should().Be("http://location-site");
            document.TrainingLocations.First().Address.Address1.Should().Be("Standard Test Address1");
            document.TrainingLocations.First().Address.Address2.Should().Be("Standard Test Address2");
            document.TrainingLocations.First().Address.Town.Should().Be("Standard Test Town");
            document.TrainingLocations.First().Address.County.Should().Be("Standard Test County");
            document.TrainingLocations.First().Address.PostCode.Should().Be("TE4 5ES");

            document.TrainingLocations.FirstOrDefault()?.LocationPoint.Latitude.Should().Be(54.213);
            document.TrainingLocations.FirstOrDefault()?.LocationPoint.Longitude.Should().Be(-52.123);
            document.TrainingLocations.FirstOrDefault()?.Location.Coordinates.Latitude.Should().Be(54.213);
            document.TrainingLocations.FirstOrDefault()?.Location.Coordinates.Longitude.Should().Be(-52.123);
            document.TrainingLocations.FirstOrDefault()?.Location.Radius.Should().Be("30mi");
        }

        [Test]
        public void WhenCreatingFrameworkDocumentShouldTrimTitleWhiteSpaces()
        {
            var frameworkMetaData = new FrameworkMetaData
            {
                EffectiveFrom = DateTime.Parse("10-Feb-14"),
                EffectiveTo = DateTime.MinValue,
                FworkCode = 616,
                PwayCode = 1,
                NasTitle = " Accounting ",
                PathwayName = "Accounting",
                ProgType = 3
            };

            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());

            var framework = mapper.CreateFrameworkDocument(frameworkMetaData);

            framework.Title.Should().Be("Accounting: Accounting");
        }

        [Test]
        public void WhenCreatingFrameworkDocumentShouldTrimPathwayWhiteSpaces()
        {
            var frameworkMetaData = new FrameworkMetaData
            {
                EffectiveFrom = DateTime.Parse("10-Feb-14"),
                EffectiveTo = DateTime.MinValue,
                FworkCode = 616,
                PwayCode = 1,
                NasTitle = "Accounting",
                PathwayName = " Accounting ",
                ProgType = 3
            };

            var mapper = new ElasticsearchMapper(_settings.Object, Mock.Of<IOrganisationTypeProcessor>());

            var framework = mapper.CreateFrameworkDocument(frameworkMetaData);

            framework.PathwayName.Should().Be("Accounting");
        }

        private Provider GenerateTestProvider()
        {
            var provider = new Provider
            {
                Id = "1234",
                Ukprn = 4556,
                Name = "Test Provider",
                MarketingInfo = "Provider Marketing",
                LearnerSatisfaction = 8.2,
                EmployerSatisfaction = 9.2,
                Frameworks = new List<FrameworkInformation>
                {
                    new FrameworkInformation
                    {
                        Code = 99,
                        PathwayCode = 1122,
                        ProgType = 20,
                        MarketingInfo = "Framework Apprenticeship Marketing",
                        InfoUrl = "http://standard-info.com",
                        ContactInformation = new ContactInformation
                        {
                            Phone = "12324-5678",
                            Email = "test@test.com",
                            Website = "http://contact-us.com"
                        },
                        DeliveryLocations = new List<DeliveryInformation>
                        {
                            new DeliveryInformation
                            {
                                DeliveryLocation = new Location
                                {
                                    Id = 77,
                                    Name = "Framework Test Location",
                                    Address = new Address
                                    {
                                        Address1 = "Framework Test Address1",
                                        Address2 = "Framework Test Address2",
                                        Town = "Framework Test Town",
                                        County = "Framework Test County",
                                        Postcode = "TE3 5ES",
                                        GeoPoint = new Coordinate { Latitude = 53.213, Longitude = -50.123 }
                                    },
                                    Contact = new ContactInformation
                                    {
                                        Website = "http://location-site"
                                    }
                                },
                                Radius = 25,
                                DeliveryModes = new List<ModesOfDelivery> { ModesOfDelivery.BlockRelease, ModesOfDelivery.DayRelease }
                            }
                        }
                    }
                },
                Standards = new List<StandardInformation>
                {
                    new StandardInformation
                    {
                        Code = 101,
                        MarketingInfo = "Standard Apprenticeship Marketing",
                        InfoUrl = "http://standard-info.com",
                        ContactInformation = new ContactInformation
                        {
                            Phone = "5555-5678",
                            Email = "test@test.com",
                            Website = "http://contact-us.com"
                        },
                        DeliveryLocations = new List<DeliveryInformation>
                        {
                            new DeliveryInformation
                            {
                                DeliveryLocation = new Location
                                {
                                    Id = 98,
                                    Name = "Standard Test Location",
                                    Address = new Address
                                    {
                                        Address1 = "Standard Test Address1",
                                        Address2 = "Standard Test Address2",
                                        Town = "Standard Test Town",
                                        County = "Standard Test County",
                                        Postcode = "TE4 5ES",
                                        GeoPoint = new Coordinate { Latitude = 54.213, Longitude = -52.123 }
                                    },
                                    Contact = new ContactInformation
                                    {
                                        Website = "http://location-site"
                                    }
                                },
                                Radius = 30,
                                DeliveryModes = new List<ModesOfDelivery> { ModesOfDelivery.BlockRelease, ModesOfDelivery.DayRelease }
                            }
                        }
                    }
                }
            };

            return provider;
        }
    }
}
