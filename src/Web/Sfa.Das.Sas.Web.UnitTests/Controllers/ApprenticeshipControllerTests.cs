﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Core.Logging;
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
            ApprenticeshipController controller = new ApprenticeshipController(null, null, null, null, null);

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

            ApprenticeshipController controller = new ApprenticeshipController(mockSearchService.Object, null, null, mockLogger.Object, mockMappingServices.Object);

            // Act
            ViewResult result = controller.SearchResults(new ApprenticeshipSearchCriteria { Keywords = "test" }) as ViewResult;

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

            ApprenticeshipController controller = new ApprenticeshipController(mockSearchService.Object, null, null, mockLogger.Object, mockMappingServices.Object);

            // Act
            ViewResult result = controller.SearchResults(new ApprenticeshipSearchCriteria { Keywords = "test" }) as ViewResult;

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

            ApprenticeshipController controller = new ApprenticeshipController(null, mockStandardRepository.Object, null, null, mockMappingServices.Object);
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

            ApprenticeshipController controller = new ApprenticeshipController(null, null, mockFrameworkRepository.Object, null, mockMappingServices.Object);
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
            ApprenticeshipController controller = new ApprenticeshipController(null, mockStandardRepository.Object, null, moqLogger.Object, null);

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
            ApprenticeshipController controller = new ApprenticeshipController(null, null, mockFrameworkRepository.Object,  moqLogger.Object, null);

            HttpNotFoundResult result = (HttpNotFoundResult)controller.Framework(1, "false");

            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Cannot find framework: 1", result.StatusDescription);
            moqLogger.Verify(m => m.Warn("404 - Cannot find framework: 1"));
        }
    }
}
