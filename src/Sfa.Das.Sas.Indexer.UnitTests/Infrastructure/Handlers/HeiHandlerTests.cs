using System.Threading.Tasks;

using Dfe.Edubase2.SoapApi.Client;
using Dfe.Edubase2.SoapApi.Client.EdubaseService;
using FluentAssertions;
using Moq;
using NUnit.Framework;

using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Handlers;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Handlers
{
    [TestFixture]
    public class HeiHandlerTests
    {
        [Test]
        public async Task ShouldQueryForHeiProviders()
        {
            var client = new Mock<IEstablishmentClient>();
            var handler = new HeiProviderHandler(Mock.Of<ILog>(), client.Object);

            EstablishmentFilter filter = null;
            client.Setup(m => m.FindEstablishmentsAsync(It.IsAny<EstablishmentFilter>()))
                .Callback<EstablishmentFilter>(model => filter = model)
                .ReturnsAsync(new EstablishmentList());

            await handler.Handle(new HeiProvidersRequest());

            filter.Page.Should().Be(0);
            filter.TypeOfEstablishment.Should().Be(EstablishmentType.HigherEducationInstitutions);
            filter.Fields.Count.Should().Be(1);
            filter.Fields.Should().Contain("UKPRN");
        }
    }
}