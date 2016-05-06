﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Collections;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Core.Logging;
using Sfa.Das.Sas.Web.Collections;
using Sfa.Das.Sas.Web.Controllers;
using Sfa.Das.Sas.Web.Models;
using Sfa.Das.Sas.Web.Services;
using Sfa.Das.Sas.Web.ViewModels;

namespace Sfa.Das.Sas.Web.UnitTests.Controllers
{
    [TestFixture]
    public sealed class ApprenticeshipControllerTests
    {
        [Test]
        public void Search_WhenNavigateTo_ShouldReturnAViewResult()
        {
            // Arrange
            ApprenticeshipController controller = new ApprenticeshipController(null, null, null, null, null, new Mock<IProfileAStep>().Object, null);

            // Act
            ViewResult result = controller.Search() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Search_WhenPassedAKeyword_ShouldReturnAViewResult()
        {
            // Arrange
            var mockSearchService = new Mock<IApprenticeshipSearchService>();
            var mockLogger = new Mock<ILog>();
            mockSearchService.Setup(x => x.SearchByKeyword(It.IsAny<string>(), 0, 10)).Returns(new ApprenticeshipSearchResults());

            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(
                x => x.Map<ApprenticeshipSearchResults, ApprenticeshipSearchResultItemViewModel>(It.IsAny<ApprenticeshipSearchResults>()))
                .Returns(new ApprenticeshipSearchResultItemViewModel());

            ApprenticeshipController controller = new ApprenticeshipController(mockSearchService.Object, null, null, mockLogger.Object, mockMappingServices.Object, new Mock<IProfileAStep>().Object, null);

            // Act
            ViewResult result = controller.SearchResults(new StandardSearchCriteria { Keywords = "test" }) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Search_WhenSearchResponseReturnsANull_ModelShouldContainTheSearchKeyword()
        {
            // Arrange
            var mockSearchService = new Mock<IApprenticeshipSearchService>();
            var mockLogger = new Mock<ILog>();
            mockSearchService.Setup(x => x.SearchByKeyword(It.IsAny<string>(), 0, 10)).Returns(value: null);

            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(
                x => x.Map<ApprenticeshipSearchResults, ApprenticeshipSearchResultViewModel>(It.IsAny<ApprenticeshipSearchResults>()))
                .Returns(new ApprenticeshipSearchResultViewModel());

            ApprenticeshipController controller = new ApprenticeshipController(mockSearchService.Object, null, null, mockLogger.Object, mockMappingServices.Object, new Mock<IProfileAStep>().Object, null);

            // Act
            ViewResult result = controller.SearchResults(new StandardSearchCriteria { Keywords = "test" }) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(null, ((ApprenticeshipSearchResultViewModel)result.Model).SearchTerm);
            Assert.IsNotNull(result);
        }

        [TestCase("true", true, Description = "Has error")]
        [TestCase("false", false, Description = "No error")]
        public void StandardDetailPageWithErrorParameter(string hasErrorParmeter, bool expected)
        {
            var mockStandardRepository = new Mock<IGetStandards>();

            var standard = new Standard { Title = "Hello", };
            mockStandardRepository.Setup(x => x.GetStandardById(It.IsAny<int>())).Returns(standard);
            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(
                x => x.Map<Standard, StandardViewModel>(It.IsAny<Standard>()))
                .Returns(new StandardViewModel());

            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.UrlReferrer).Returns(new Uri("http://www.abba.co.uk"));

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(mockRequest.Object);
            
            ApprenticeshipController controller = new ApprenticeshipController(null, mockStandardRepository.Object, null, null, mockMappingServices.Object, new Mock<IProfileAStep>().Object, new Mock<IListCollection<int>>().Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            controller.Url = new UrlHelper(
                new RequestContext(context.Object, new RouteData()),
                new RouteCollection());

            var result = controller.Standard(1, hasErrorParmeter) as ViewResult;

            Assert.NotNull(result);
            var actual = ((StandardViewModel)result.Model).HasError;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("true", true, Description = "Has error")]
        [TestCase("false", false, Description = "No error")]
        public void FrameworkDetailPageWithErrorParameter(string hasErrorParmeter, bool expected)
        {
            var mockFrameworkRepository = new Mock<IGetFrameworks>();

            var stubFrameworkViewModel = new FrameworkViewModel
            {
                FrameworkId = 123,
                Title = "Title test",
                FrameworkName = "Framework name test",
                FrameworkCode = 321,
                Level = 3,
                PathwayName = "Pathway name test",
                PathwayCode = 4
            };

            var framework = new Framework { Title = "Hello", };
            mockFrameworkRepository.Setup(x => x.GetFrameworkById(It.IsAny<int>())).Returns(framework);
            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(
                x => x.Map<Framework, FrameworkViewModel>(It.IsAny<Framework>()))
                .Returns(stubFrameworkViewModel);

            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.UrlReferrer).Returns(new Uri("http://www.abba.co.uk"));

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(mockRequest.Object);

            ApprenticeshipController controller = new ApprenticeshipController(null, null, mockFrameworkRepository.Object, null, mockMappingServices.Object, new Mock<IProfileAStep>().Object, null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            controller.Url = new UrlHelper(
                new RequestContext(context.Object, new RouteData()),
                new RouteCollection());

            var result = controller.Framework(1, hasErrorParmeter) as ViewResult;

            Assert.NotNull(result);
            var actual = ((FrameworkViewModel)result.Model).HasError;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StandardDetailPageStandardIsNull()
        {
            var mockStandardRepository = new Mock<IGetStandards>();

            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.UrlReferrer).Returns(new Uri("http://www.abba.co.uk"));
            var moqLogger = new Mock<ILog>();
            ApprenticeshipController controller = new ApprenticeshipController(null, mockStandardRepository.Object, null, moqLogger.Object, null, new Mock<IProfileAStep>().Object, null);

            HttpNotFoundResult result = (HttpNotFoundResult)controller.Standard(1, "false");

            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Cannot find standard: 1", result.StatusDescription);
            moqLogger.Verify(m => m.Warn("404 - Cannot find standard: 1"));
        }

        [Test]
        public void FrameworkDetailPageStandardIsNull()
        {
            var mockFrameworkRepository = new Mock<IGetFrameworks>();

            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.UrlReferrer).Returns(new Uri("http://www.abba.co.uk"));
            var moqLogger = new Mock<ILog>();
            ApprenticeshipController controller = new ApprenticeshipController(null, null, mockFrameworkRepository.Object,  moqLogger.Object, null, new Mock<IProfileAStep>().Object, null);

            HttpNotFoundResult result = (HttpNotFoundResult)controller.Framework(1, "false");

            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Cannot find framework: 1", result.StatusDescription);
            moqLogger.Verify(m => m.Warn("404 - Cannot find framework: 1"));
        }

        [Test]
        public void ShouldAddStandardToShortListIfRequested()
        {
            // Arrange
            var mockStandardRepository = new Mock<IGetStandards>();
            mockStandardRepository.Setup(x => x.GetStandardById(It.IsAny<int>())).Returns(new Standard());

            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(x => x.Map<Standard, StandardViewModel>(It.IsAny<Standard>()))
                                            .Returns(new StandardViewModel());

            var mockCookieRepository = new Mock<IListCollection<int>>();
            var controller = new ApprenticeshipController(
                null,
                mockStandardRepository.Object,
                null,
                null,
                mockMappingServices.Object,
                new Mock<IProfileAStep>().Object,
                mockCookieRepository.Object);

            const int standardId = 5;
            mockCookieRepository.Setup(x => x.AddItem(ApprenticeshipController.StandardsShortListCookieName, standardId));

            // Act
            var result = controller.StandardShortList(standardId, "save");

            // Assert
            Assert.IsNotNull(result);
            mockCookieRepository.Verify(x => x.AddItem(ApprenticeshipController.StandardsShortListCookieName, standardId), Times.Once());
        }
        
        [Test]
        public void ShouldRemoveStandardFromShortListIfRequested()
        {
            // Arrange
            var mockStandardRepository = new Mock<IGetStandards>();
            mockStandardRepository.Setup(x => x.GetStandardById(It.IsAny<int>())).Returns(new Standard());

            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(x => x.Map<Standard, StandardViewModel>(It.IsAny<Standard>()))
                                            .Returns(new StandardViewModel());

            var mockCookieRepository = new Mock<IListCollection<int>>();
            var controller = new ApprenticeshipController(
                null,
                mockStandardRepository.Object,
                null,
                null,
                mockMappingServices.Object,
                new Mock<IProfileAStep>().Object,
                mockCookieRepository.Object);

            const int standardId = 5;
            mockCookieRepository.Setup(x => x.RemoveItem(ApprenticeshipController.StandardsShortListCookieName, standardId));

            // Act
            var result = controller.StandardShortList(standardId, "remove");

            // Assert
            Assert.IsNotNull(result);
            mockCookieRepository.Verify(x => x.RemoveItem(ApprenticeshipController.StandardsShortListCookieName, standardId), Times.Once());
        }

        [Test]
        public void ShouldNotAddStandardToShortListIfStandardCannotBeFound()
        {
            // Arrange
            var mockStandardRepository = new Mock<IGetStandards>();
            mockStandardRepository.Setup(x => x.GetStandardById(It.IsAny<int>()));
            
            var mockCookieRepository = new Mock<IListCollection<int>>();
            var controller = new ApprenticeshipController(
                null,
                mockStandardRepository.Object,
                null,
                null,
                null,
                new Mock<IProfileAStep>().Object,
                mockCookieRepository.Object);

            const int standardId = 5;
            mockCookieRepository.Setup(x => x.AddItem(ApprenticeshipController.StandardsShortListCookieName, standardId));

            // Act
            var result = controller.StandardShortList(standardId, "save");

            // Assert
            Assert.IsNotNull(result);
            mockCookieRepository.Verify(x => x.AddItem(ApprenticeshipController.StandardsShortListCookieName, standardId), Times.Never);
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, 2, true, Description = "Shortlisted")]
        [TestCase(new[] { 1, 3 }, 2, false, Description = "Not Shortlisted")]
        public void ShouldSetViewModelShortListValueToTrueIfStandardIsInShortList(
            IEnumerable<int> shortListItems,
            int standardId,
            bool expectedResult)
        {
            // Arrange
            var mockStandardRepository = new Mock<IGetStandards>();
            mockStandardRepository.Setup(x => x.GetStandardById(It.IsAny<int>())).Returns(new Standard());

            var mockMappingServices = new Mock<IMappingService>();
            mockMappingServices.Setup(x => x.Map<Standard, StandardViewModel>(It.IsAny<Standard>()))
                                            .Returns(new StandardViewModel());

            var mockCookieRepository = new Mock<IListCollection<int>>();
            var controller = new ApprenticeshipController(
                null,
                mockStandardRepository.Object,
                null,
                null,
                mockMappingServices.Object,
                new Mock<IProfileAStep>().Object,
                mockCookieRepository.Object);

            AddUrlMocking(controller, "http://www.abba.co.uk");

            mockCookieRepository.Setup(x => x.GetAllItems(ApprenticeshipController.StandardsShortListCookieName))
                                .Returns(new List<int>(shortListItems));

            // Act
            var result = controller.Standard(standardId, string.Empty) as ViewResult;
            var viewModel = result?.Model as StandardViewModel;

            // Assert
            Assert.IsNotNull(viewModel);
            mockCookieRepository.Verify(x => x.GetAllItems(ApprenticeshipController.StandardsShortListCookieName));
            Assert.AreEqual(expectedResult, viewModel.IsShortlisted);
        }
        
        private static void AddUrlMocking(ApprenticeshipController controller, string url)
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.UrlReferrer).Returns(new Uri(url));

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(mockRequest.Object);

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            controller.Url = new UrlHelper(
                new RequestContext(context.Object, new RouteData()),
                new RouteCollection());
        }
    }
}