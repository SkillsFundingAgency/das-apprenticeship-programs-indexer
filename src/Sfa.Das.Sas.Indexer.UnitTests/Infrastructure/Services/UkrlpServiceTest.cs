using Ukrlp.SoapApi.Client;
using Ukrlp.SoapApi.Client.ProviderQueryServiceV4;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    [TestFixture]
    public class UkrlpServiceTest
    {
        [Test]
        public void ShouldReturnProviderResults()
        {
            Mock<IInfrastructureSettings> mockInfrastructureSettings = new Mock<IInfrastructureSettings>();
            Mock<IProviderQueryApiClient> mockProviderQueryPortTypeClientWrapper = new Mock<IProviderQueryApiClient>();

            mockInfrastructureSettings.SetupGet(x => x.UkrlpQueryId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpStakeholderId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpProviderStatus).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpRequestUkprnBatchSize).Returns(2);

            mockProviderQueryPortTypeClientWrapper.Setup(x => x.ProviderQuery(It.IsAny<SelectionCriteriaStructure>(), "2", 35)).Returns(GetClientResponseMockValues());

            var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, Mock.Of<ILog>());

            var models = sut.Handle(new UkrlpProviderRequest { Providers = new List<int> { 1234 } });

            Assert.AreEqual(2, models.MatchingProviderRecords.Count());
        }

        private static IEnumerable<Ukrlp.SoapApi.Types.Provider> GetClientResponseMockValues()
        {
            yield return new Ukrlp.SoapApi.Types.Provider
            {
                ProviderName = "Abc Ltd",
                ProviderAliases = new List<string>
                {
                    "ABC",
                    "abc trading"
                },
                ProviderContact = new[]
                {
                    new Ukrlp.SoapApi.Types.Contact
                    {
                        ContactType = "P",
                        Address = new Ukrlp.SoapApi.Types.Address
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
            yield return new Ukrlp.SoapApi.Types.Provider { UnitedKingdomProviderReferenceNumber = "5678" };
        }
    }
}