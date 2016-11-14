﻿using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.ApplicationServices.Infrastructure;
using Sfa.Das.Sas.Indexer.ApplicationServices.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Settings;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.CourseDirectory
{
    [TestFixture]
    public class CourseDirectoryClientTest
    {
        private IEnumerable<Provider> _providers;

        [OneTimeSetUp]
        public void Setup()
        {
            var moqSettings = new Mock<IInfrastructureSettings>();
            var moqAppServiceSettings = new Mock<IAppServiceSettings>();
            moqSettings.Setup(m => m.CourseDirectoryUri).Returns("http://www.fake.url/to.course/directory");
            var moqLogger = new Mock<ILog>();
            var moqConvertFromCsv = new Mock<IConvertFromCsv>();
            var moqVstsService = new Mock<IVstsClient>();
            var courseDirectoryClient = new CourseDirectoryClient(
                moqSettings.Object,
                new Stub.StubCourseDirectoryResponseClient(),
                moqConvertFromCsv.Object,
                moqVstsService.Object,
                moqAppServiceSettings.Object,
                moqLogger.Object);
            _providers = courseDirectoryClient.GetApprenticeshipProvidersAsync().Result;
        }

        [Test]
        public void ShouldMapProviders()
        {
            var frameworks = _providers.SelectMany(m => m.Frameworks);
            var standards = _providers.SelectMany(m => m.Standards);
            _providers.Count().Should().Be(6);
            frameworks.Count().Should().Be(71);
            standards.Count().Should().Be(4);
        }

        [Test(Description = "Testing mapping for first provider in stub data - SWINDON COLLEGE")]
        public void ShouldMapSwindoncollegeProvider()
        {
            var first = _providers.FirstOrDefault();

            first.Name.Should().Be("SWINDON COLLEGE");

            first.Frameworks.Count().Should().Be(6);
            first.Standards.Count().Should().Be(1);

            var fw = first.Frameworks.Where(m => m.Code.Equals(436) && m.ProgType.Equals(3) && m.PathwayCode.Equals(1));
            fw.Count().Should().Be(1);
            var fw43631 = fw.FirstOrDefault();
            fw43631.DeliveryLocations.Count().Should().Be(1);

            var deliveryLocation = fw43631.DeliveryLocations.FirstOrDefault();
            deliveryLocation.DeliveryModes.Contains(ModesOfDelivery.BlockRelease).Should().BeTrue();
            deliveryLocation.DeliveryModes.Contains(ModesOfDelivery.DayRelease).Should().BeTrue();
            deliveryLocation.DeliveryModes.Contains(ModesOfDelivery.OneHundredPercentEmployer).Should().BeFalse();
        }
    }
}