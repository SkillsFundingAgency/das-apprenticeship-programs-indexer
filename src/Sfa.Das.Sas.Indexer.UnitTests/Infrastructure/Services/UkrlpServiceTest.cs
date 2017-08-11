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
    using Ukrlp.SoapApi.Client.Exceptions;

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

        //[Test]
        //public void ShouldReturnProviderResults()
        //{
        //    mockProviderQueryPortTypeClientWrapper.Setup(x => x.ProviderQuery(It.IsAny<SelectionCriteriaStructure>(), "2", 35)).Returns(new ProviderResponse { Providers = GetClientResponseMockValues(), Warnings = new Dictionary<string, string>() });

        //    var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, mockLog.Object);

        //    var models = sut.Handle(new UkrlpProviderRequest { Providers = new List<int> { 12344321, 56788765 } });

        //    Assert.AreEqual(2, models.MatchingProviderRecords.Count());

        //    mockLog.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(2));
        //}

        [Test]
        public void ShouldThrowApplicationErrorIfUKRLPServiceisDown()
        {
            mockProviderQueryPortTypeClientWrapper.Setup(x => x.ProviderQuery(It.IsAny<SelectionCriteriaStructure>(), "2", 35))
                 .Throws(new ProviderQueryException("Request timed Out", new SelectionCriteriaStructure { UnitedKingdomProviderReferenceNumberList = new string[] { "1234", "5678" } }, new Exception()));

            var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, mockLog.Object);

            Assert.Throws<ApplicationException>(() => sut.Handle(new UkrlpProviderRequest { Providers = new List<int> { 1234, 5678 } }));
        }

        //[Test]
        //public void ShouldHandleAnyWarningsFromUKRLPService()
        //{
        //    mockProviderQueryPortTypeClientWrapper.Setup(x => x.ProviderQuery(It.IsAny<SelectionCriteriaStructure>(), "2", 35))
        //        .Returns(new ProviderResponse
        //        {
        //            Providers = GetClientResponseMockValues(),
        //            Warnings = new Dictionary<string, string>() { { "1000", "the ukprn wasn't 8 digits long" }, { "5000", "the ukprn wasn't 8 digits long" } }
        //        });

        //    var sut = new UkrlpService(mockInfrastructureSettings.Object, mockProviderQueryPortTypeClientWrapper.Object, mockLog.Object);

        //    var models = sut.Handle(new UkrlpProviderRequest { Providers = new List<int> { 12344321, 56788765 } });

        //    Assert.AreEqual(2, models.MatchingProviderRecords.Count());

        //    mockLog.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Exactly(2));
        //}

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