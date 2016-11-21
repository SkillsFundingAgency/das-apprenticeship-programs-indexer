using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using Sfa.Das.Sas.Indexer.Infrastructure.Uklrp;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    [TestFixture]
    public class UkrlpServiceTest
    {
        [Test]
        public void ShouldReturnProviderResults()
        {
            Mock<IInfrastructureSettings> mockInfrastructureSettings = new Mock<IInfrastructureSettings>();
            Mock<IProviderQueryPortTypeClientWrapper> mockProviderQueryPortTypeClientWrapper = new Mock<IProviderQueryPortTypeClientWrapper>();

            mockInfrastructureSettings.SetupGet(x => x.UkrlpQueryId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpStakeholderId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpProviderStatus).Returns(It.IsAny<string>());

            mockProviderQueryPortTypeClientWrapper.Setup(x => x.RetrieveAllProvidersAsync(It.IsAny<ProviderQueryStructure>())).Returns(Task.FromResult(GetClientResponseMockValues()));

            var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, new UkrlpProviderResponseMapper(), Mock.Of<ILog>());

            var models = Task.Run(() => sut.GetLearnerProviderInformationAsync(new [] { "1234" }));

            Assert.AreEqual(2, models.Result.Count);
        }

        private static response GetClientResponseMockValues()
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
                new ProviderRecordStructure {UnitedKingdomProviderReferenceNumber = "5678"}
            };

            ProviderQueryResponse pqr = new ProviderQueryResponse { MatchingProviderRecords = prs };

            return new response(pqr);
        }
    }
}