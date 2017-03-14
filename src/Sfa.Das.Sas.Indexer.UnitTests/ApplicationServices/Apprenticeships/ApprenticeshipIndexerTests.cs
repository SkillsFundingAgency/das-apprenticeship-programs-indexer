﻿namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationServices.Services
{
    using System;
    using MediatR;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

    [TestFixture]
    public sealed class ApprenticeshipIndexerTests
    {
        private ApprenticeshipIndexer _sut;
        private Mock<IIndexSettings<IMaintainApprenticeshipIndex>> _mockSettings;
        private Mock<IMaintainApprenticeshipIndex> _mockIndexMaintainer;
        private Mock<IMetaDataHelper> _mockMetaDataHelper;
        private Mock<IMediator> _mockMediator;

        [SetUp]
        public void Setup()
        {
            _mockSettings = new Mock<IIndexSettings<IMaintainApprenticeshipIndex>>();
            _mockIndexMaintainer = new Mock<IMaintainApprenticeshipIndex>();
            _mockMetaDataHelper = new Mock<IMetaDataHelper>();
            _mockMediator = new Mock<IMediator>();
            var mockLogger = Mock.Of<ILog>();

            _sut = new ApprenticeshipIndexer(_mockSettings.Object, _mockMediator.Object, _mockIndexMaintainer.Object, _mockMetaDataHelper.Object, mockLogger);
        }

        [Test]
        public void ShouldCreateIndexIfOneDoesNotAlreadyExist()
        {
            _sut.CreateIndex("tetsindex");

            _mockIndexMaintainer.Verify(x => x.CreateIndex(It.IsAny<string>()), Times.Once);
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
