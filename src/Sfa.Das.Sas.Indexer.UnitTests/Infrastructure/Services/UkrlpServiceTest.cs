using Ukrlp.SoapApi.Client.Exceptions;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Ukrlp.SoapApi.Client;
    using Ukrlp.SoapApi.Client.ProviderQueryServiceV4;
    using Ukrlp.SoapApi.Types;

    [TestFixture]
    public class UkrlpServiceTest
    {
        private Mock<IInfrastructureSettings> mockInfrastructureSettings;
        private Mock<IProviderQueryApiClient> mockProviderQueryPortTypeClientWrapper;
        private Mock<ILog> mockLog;

        [SetUp]
        public void TestSetUp()
        {
            mockInfrastructureSettings = new Mock<IInfrastructureSettings>();
            mockProviderQueryPortTypeClientWrapper = new Mock<IProviderQueryApiClient>();
            mockLog = new Mock<ILog>();
            mockInfrastructureSettings.SetupGet(x => x.UkrlpStakeholderId).Returns(It.IsAny<string>());
            mockInfrastructureSettings.SetupGet(x => x.UkrlpProviderStatus).Returns(It.IsAny<string>());
        }

        [Test]
        public void ShouldThrowAnExceptionIfThereWasAnExceptionFromUkrlp()
        {
            mockProviderQueryPortTypeClientWrapper.Setup(x => x.ProviderQuery(It.IsAny<SelectionCriteriaStructure>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new ProviderQueryException(It.IsAny<string>(), new SelectionCriteriaStructure { UnitedKingdomProviderReferenceNumberList = new[] { "1234", "5678" } }, It.IsAny<Exception>()));

            var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, mockLog.Object);

            Assert.Throws<ProviderQueryException>(() => sut.Handle(new UkrlpProviderRequest { Providers = new List<int> { 1234, 5678 } }));
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
                UnitedKingdomProviderReferenceNumber = "12344321"
            };
            yield return new Ukrlp.SoapApi.Types.Provider { UnitedKingdomProviderReferenceNumber = "56788765" };
        }
    }
}