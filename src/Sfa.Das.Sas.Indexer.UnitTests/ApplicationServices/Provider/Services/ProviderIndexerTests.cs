﻿using System;
using System.Globalization;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationServices.Provider.Services
{
    [TestFixture]
    public sealed class ProviderIndexerTests
    {
        private ProviderIndexer _sut;

        private Mock<IIndexSettings<IMaintainProviderIndex>> _mockSettings;
        private Mock<IMaintainProviderIndex> _mockIndexMaintainer;
        private Mock<ICourseDirectoryProviderMapper> _mockCourseDirectoryProviderMapper;
        private Mock<IUkrlpProviderMapper> _mockUkrlpProviderMapper;

        [SetUp]
        public void Setup()
        {
            _mockSettings = new Mock<IIndexSettings<IMaintainProviderIndex>>();
            _mockIndexMaintainer = new Mock<IMaintainProviderIndex>();
            _mockCourseDirectoryProviderMapper = new Mock<ICourseDirectoryProviderMapper>();
            _mockUkrlpProviderMapper = new Mock<IUkrlpProviderMapper>();

            _sut = new ProviderIndexer(
                _mockSettings.Object,
                _mockCourseDirectoryProviderMapper.Object,
                _mockUkrlpProviderMapper.Object,
                _mockIndexMaintainer.Object,
                Mock.Of<IProviderDataService>(),
                Mock.Of<ILog>());
        }

        [Test]
        public void ShouldCreateIndexIfOneDoesNotAlreadyExist()
        {
            _sut.CreateIndex("testindex");

            _mockIndexMaintainer.Verify(x => x.CreateIndex(It.IsAny<string>()), Times.Once);
        }

        [TestCase("13/03/2017", null, true)]
        [TestCase("16/03/2017", null, false)]
        [TestCase(null, null, false)]
        [TestCase("13/03/2017", "16/03/2017", true)]
        [TestCase("11/03/2017", "13/03/2017", false)]
        public void should(string start, string end, bool expected)
        {
            DateTime? startDate = convertDate(start);
            DateTime? endDate = convertDate(end);
            RoatpProviderResult roatpProvider = new RoatpProviderResult
            {
                StartDate = startDate,
                EndDate = endDate
            };

            Assert.AreEqual(expected, _sut.IsDateValid(roatpProvider));
        }

        private DateTime? convertDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }

            return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        [Test]
        public void ShouldCreateIndexWithTheCorrectName()
        {
            const string testAliasName = "TestAlias";
            _mockSettings.SetupGet(x => x.IndexesAlias).Returns(testAliasName);
            _sut.CreateIndex("testalias-2016-05-10-14");

            _mockIndexMaintainer.Verify(x => x.CreateIndex(It.Is<string>(y => y == $"testalias-2016-05-10-14")), Times.Once);
        }

        [Test]
        public void CreatIndexShouldDeleteAnyExistingIndexWithTheSameName()
        {
            _mockIndexMaintainer.Setup(x => x.IndexExists(It.IsAny<string>())).Returns(true);
            _sut.CreateIndex("testindex");

            _mockIndexMaintainer.Verify(x => x.DeleteIndex(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void SwapIndexesShouldSwapTheIndexUsedToTheProvidedOne()
        {
            const string testAliasName = "TestAlias";
            _mockSettings.SetupGet(x => x.IndexesAlias).Returns(testAliasName);
            _mockIndexMaintainer.Setup(x => x.AliasExists(It.IsAny<string>())).Returns(true);
            _sut.ChangeUnderlyingIndexForAlias("testalias-2016-05-10-14");

            _mockIndexMaintainer.Verify(x => x.SwapAliasIndex(It.Is<string>(y => y == "TestAlias"), It.Is<string>(z => z == "testalias-2016-05-10-14")), Times.Once);
        }

        [Test]
        public void SwapIndexesShouldCreateAliasIfItDoesNotExist()
        {
            const string testAliasName = "TestAlias";
            _mockSettings.SetupGet(x => x.IndexesAlias).Returns(testAliasName);
            _mockIndexMaintainer.Setup(x => x.AliasExists(It.IsAny<string>())).Returns(false);
            _sut.ChangeUnderlyingIndexForAlias("testalias-2016-05-10-14");

            _mockIndexMaintainer.Verify(x => x.CreateIndexAlias(It.Is<string>(z => z == "TestAlias"), It.Is<string>(y => y == "testalias-2016-05-10-14")), Times.Once);
        }

        [Test]
        public void DeleteIndexesShouldDeleteIndexesOfLastTwoDays()
        {
            Func<string, bool> matcher = null;
            var testDate = new DateTime(2016, 5, 10, 14, 33, 30, DateTimeKind.Utc);
            const string testAliasName = "TestAlias";
            _mockSettings.SetupGet(x => x.IndexesAlias).Returns(testAliasName);
            _mockIndexMaintainer.Setup(x => x.DeleteIndexes(It.IsAny<Func<string, bool>>())).Callback<Func<string, bool>>(y => matcher = y);

            _sut.DeleteOldIndexes(testDate);

            _mockIndexMaintainer.Verify(x => x.DeleteIndexes(It.IsAny<Func<string, bool>>()), Times.Once);
            var match1 = matcher.Invoke("testalias-2016-05-09");
            var match2 = matcher.Invoke("testalias-2016-05-08");
            var match3 = matcher.Invoke("testalias-2016-05-07");
            var match4 = matcher.Invoke("testalias-2016-05-06");
            var match5 = matcher.Invoke("wrongtestalias-2016-05-06");

            Assert.That(match1, Is.False);
            Assert.That(match2, Is.True);
            Assert.That(match3, Is.True);
            Assert.That(match4, Is.True);
            Assert.That(match5, Is.False);
        }
    }
}