namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.Core.Logging;
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

            var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, Mock.Of<ILog>());

            var models = sut.Handle(new UkrlpProviderRequest { Providers = new List<int> { 1234 } });

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
            yield return new Provider { UnitedKingdomProviderReferenceNumber = "5678" };
        }
    }
}