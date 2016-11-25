using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

    [TestFixture]
    public class UkrlpServiceTest
    {
        [Test]
        public void ShouldReturnProviderResults()
        {
            Mock<IInfrastructureSettings> mockInfrastructureSettings = new Mock<IInfrastructureSettings>();
            Mock<IUkrlpClient> mockProviderQueryPortTypeClientWrapper = new Mock<IUkrlpClient>();

            mockInfrastructureSettings.SetupGet(x => x.UkrlpQueryId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpStakeholderId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpProviderStatus).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpRequestUkprnBatchSize).Returns(2);

            mockProviderQueryPortTypeClientWrapper.Setup(x => x.RetrieveAllProviders(It.IsAny<ProviderQueryStructure>())).Returns(GetClientResponseMockValues());

            var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, Mock.Of<ILogProvider>());

            var models = sut.GetProviders(new List<int> { 1234 });
            
            Assert.AreEqual(2, models.MatchingProviderRecords.Count());
        }

        private static IEnumerable<Provider> GetClientResponseMockValues()
        {
            yield return new Provider
            {
                ProviderName = "Abc Ltd",
                ProviderContact = new[]
                {
                    new ProviderContact
                    {
                        ContactType = "P",
                        ContactAddress = new ContactAddress
                        {
                            StreetDescription = "Down a Road",
                            PostTown = "Coventry",
                            PostCode = "EY6 7GH",
                            PAON = "sdfsdf",
                            SAON = "sdfsdf"
                        }
                    }
                },
                UnitedKingdomProviderReferenceNumber = "1234"
            };
            yield return new Provider { UnitedKingdomProviderReferenceNumber = "5678"};
        }

        private static response GetClientResponseMockValues2()
        {
            ProviderRecordStructure[] prs = {
                new ProviderRecordStructure
                {
                    ProviderName = "Abc Ltd",
                    ProviderContact = new [] 
                    {
                        new ProviderContactStructure
                        {
                            ContactType = "P",
                            ContactAddress = new BSaddressStructure
                            {
                                StreetDescription = "Down a Road",
                                PostTown = "Coventry",
                                PostCode = "EY6 7GH",
                                PAON = new AONstructure { Description = "sdfsdf" },
                                SAON = new AONstructure { Description = "sdfsdf" }
                            }
                        }
                    },
                    UnitedKingdomProviderReferenceNumber = "1234"
                },
                new ProviderRecordStructure { UnitedKingdomProviderReferenceNumber = "5678" }
            };

            ProviderQueryResponse pqr = new ProviderQueryResponse { MatchingProviderRecords = prs };

            return new response(pqr);
        }
    }
}