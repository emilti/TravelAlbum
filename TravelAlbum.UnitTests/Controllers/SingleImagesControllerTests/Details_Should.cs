using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Controllers;
using TravelAlbum.Web.Models.ImageModels;
using TravelAlbum.Web.Models.SingleImageModels;

namespace TravelAlbum.UnitTests.Controllers.SingleImagesControllerTests
{

    [TestClass]
    public class Details_Should
    {
        [TestMethod]
        public void ReturnView_WhenValidGuidIsPassedAndLanuageIsBg()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var singleImageTranslationalInfoService = new Mock<ISingleImageTranslationalInfoService>();

            Guid singleImageId = Guid.NewGuid();

            SingleImage singleImageObjectMock = new SingleImage()
            {
                TravelObjectId = singleImageId,
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };

            SingleImageTranslationalInfo singleImageTranslationalInfoMock =
            new SingleImageTranslationalInfo()
            {
                TravelObjectId = singleImageObjectMock.TravelObjectId,
                SingleImage = singleImageObjectMock,
                SingleImageTranslationalInfoId = Guid.NewGuid(),
                Description = "Тест описание снимка",
                Language = Language.Bulgarian
            };

            singleImageObjectMock.TranslatedInfoes.Add(singleImageTranslationalInfoMock);

            singleImageServiceMock.Setup(
                m => m.GetById((Guid?)singleImageObjectMock.TravelObjectId))
                .Returns(new SingleImage()
                {
                    TravelObjectId = singleImageObjectMock.TravelObjectId,
                    CreatedOn = singleImageObjectMock.CreatedOn,
                    Content = singleImageObjectMock.Content,
                    TranslatedInfoes =
                    {
                            singleImageTranslationalInfoMock
                    }
                });

            SingleImagesController singleImagesController =
                 new SingleImagesController(
                singleImageServiceMock.Object,
                mountainsServiceMock.Object,
                singleImageTranslationalInfoService.Object);

            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/bg/SingleImages/Details/79cd1d5e-d2c2-425a-844b-0a0535b951e6", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            singleImagesController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), singleImagesController);

            // Act & Assert
            singleImagesController
            .WithCallTo(b => b.Details(singleImageObjectMock.TravelObjectId))
                .ShouldRenderDefaultView()
                .WithModel<ImageOutputViewModel>(viewModel =>
                {
                    Assert.AreEqual(singleImageTranslationalInfoMock.Description, viewModel.Description);
                    Assert.AreEqual(singleImageObjectMock.CreatedOn, viewModel.CreatedOn);
                });
        }

        [TestMethod]
        public void ReturnView_WhenValidGuidIsPassedAndLanuageIsEn()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var singleImageTranslationalInfoService = new Mock<ISingleImageTranslationalInfoService>();

            Guid singleImageId = Guid.NewGuid();

            SingleImage singleImageObjectMock = new SingleImage()
            {
                TravelObjectId = singleImageId,
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 09, 09)
            };

            SingleImageTranslationalInfo singleImageTranslationalInfoMock =
            new SingleImageTranslationalInfo()
            {
                TravelObjectId = singleImageObjectMock.TravelObjectId,
                SingleImage = singleImageObjectMock,
                SingleImageTranslationalInfoId = Guid.NewGuid(),
                Description = "Test description photo",
                Language = Language.English
            };

            singleImageObjectMock.TranslatedInfoes.Add(singleImageTranslationalInfoMock);

            singleImageServiceMock.Setup(
                m => m.GetById((Guid?)singleImageObjectMock.TravelObjectId))
                .Returns(new SingleImage()
                {
                    TravelObjectId = singleImageObjectMock.TravelObjectId,
                    CreatedOn = singleImageObjectMock.CreatedOn,
                    Content = singleImageObjectMock.Content,
                    TranslatedInfoes =
                    {
                         singleImageTranslationalInfoMock
                    }
                });

            SingleImagesController singleImagesController =
                 new SingleImagesController(
                singleImageServiceMock.Object,
                mountainsServiceMock.Object,
                singleImageTranslationalInfoService.Object);

            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/en/SingleImages/Details/79cd1d5e-d2c2-425a-844b-0a0535b951e6", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            singleImagesController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), singleImagesController);

            // Act & Assert
            singleImagesController
            .WithCallTo(b => b.Details(singleImageObjectMock.TravelObjectId))
                .ShouldRenderDefaultView()
                .WithModel<ImageOutputViewModel>(viewModel =>
                {
                    Assert.AreEqual(singleImageTranslationalInfoMock.Description, viewModel.Description);
                    Assert.AreEqual(singleImageObjectMock.CreatedOn, new DateTime(2017, 09, 09));
                });
        }

        [TestMethod]
        public void ReturnView_WhenTravelGuidNotMatchWithExistingTravel()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var singleImageTranslationalInfoService = new Mock<ISingleImageTranslationalInfoService>();

            Guid id = Guid.NewGuid();

            singleImageServiceMock.Setup(m => m.GetById((Guid?)null)).Returns((SingleImage)null);

            SingleImagesController singleImagesController = new SingleImagesController(singleImageServiceMock.Object, mountainsServiceMock.Object, singleImageTranslationalInfoService.Object);

            // Act and Assert
            singleImagesController.WithCallTo(
                b => b.Details(id))
                  .ShouldRedirectTo<HomeController>(typeof(HomeController)
                  .GetMethod("Index"));

        }
    }
}
