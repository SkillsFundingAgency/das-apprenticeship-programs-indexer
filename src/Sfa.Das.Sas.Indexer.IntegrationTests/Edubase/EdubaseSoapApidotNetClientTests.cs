namespace Sfa.Das.Sas.Indexer.IntegrationTests.Edubase
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Dfe.Edubase2.SoapApi.Client;
    using Dfe.Edubase2.SoapApi.Client.EdubaseService;
    using NUnit.Framework;

    [TestFixture]
    
    public class EdubaseSoapApidotNetClientTests
    {
        private EstablishmentClient _client;
        [SetUp]
        public void TestSetup()
        {
            string username = ConfigurationManager.AppSettings["EduBaseUsername"];

            string password = ConfigurationManager.AppSettings["EduBasePassword"];

            _client = new EstablishmentClient(username, password);
        }

        [Test]
        [Ignore("Need to edit vstsbuild definition to transform EduBaseUsername and EduBasePassword in app.config")]
        public void ShouldReturnAHigherEducationInstitution()
        {
            var hei =
                _client.FindEstablishments(new EstablishmentFilter
                {
                    TypeOfEstablishment = EstablishmentType.HigherEducationInstitutions,
                }).ToList();

            var results = _client.GetEstablishment(hei.First().URN);

            Assert.IsNotNull(results);
        }

        [Test]
        [Ignore("Need to edit vstsbuild definition to transform EduBaseUsername and EduBasePassword in app.config")]
        public async Task ShouldReturnAHigherEducationInstitutionAsync()
        {
            var hei =
                _client.FindEstablishments(new EstablishmentFilter
                {
                    TypeOfEstablishment = EstablishmentType.HigherEducationInstitutions,
                }).ToList();

            var results = await _client.GetEstablishmentAsync(hei.First().URN);

            Assert.IsNotNull(results);
        }

        [Test]
        [Ignore("Need to edit vstsbuild definition to transform EduBaseUsername and EduBasePassword in app.config")]
        public async Task ShouldhaveEqualNoofHigherEducationInstitutions()
        {
            var results =
                _client.FindEstablishments(new EstablishmentFilter
                {
                    TypeOfEstablishment = EstablishmentType.HigherEducationInstitutions
                }).ToList();

            var resultsasync = await
                _client.FindEstablishmentsAsync(new EstablishmentFilter
                {
                    TypeOfEstablishment = EstablishmentType.HigherEducationInstitutions
                });

            Assert.True(results.Count() == resultsasync.Count());
        }
    }
}
