using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Controllers;
using TravelAlbum.Web.Models.ImageModels;
using TravelAlbum.Web.Utils;

namespace TravelAlbum.UnitTests.Controllers.ImagesControllerTests
{

    [TestClass]
    public class Details_Should
    {
        [TestMethod]
        public void ReturnView_WhenValidGuidIsPassedAndLanuageIsBg()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var imageTranslationalInfoService = new Mock<IImageTranslationalInfoService>();
            var travelServiceMock = new Mock<ITravelService>();
            var utilsMock = new Mock<IUtils>();

            Guid imageId = Guid.NewGuid();

            Image imageObjectMock = new Image()
            {
                TravelObjectId = imageId,
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };

            ImageTranslationalInfo imageTranslationalInfoMock =
            new ImageTranslationalInfo()
            {
                TravelObjectId = imageObjectMock.TravelObjectId,
                Image = imageObjectMock,
                ImageTranslationalInfoId = Guid.NewGuid(),
                Description = "Тест описание снимка",
                Language = Language.Bulgarian
            };

            imageObjectMock.TranslatedInfoes.Add(imageTranslationalInfoMock);

            imageServiceMock.Setup(
                m => m.GetById((Guid?)imageObjectMock.TravelObjectId))
                .Returns(new Image()
                {
                    TravelObjectId = imageObjectMock.TravelObjectId,
                    CreatedOn = imageObjectMock.CreatedOn,
                    Content = imageObjectMock.Content,
                    TranslatedInfoes =
                    {
                            imageTranslationalInfoMock
                    }
                });

            ImagesController imagesController =
                 new ImagesController(
                imageServiceMock.Object,
                mountainsServiceMock.Object,
                imageTranslationalInfoService.Object,
                travelServiceMock.Object,
                utilsMock.Object);

            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/bg/Images/Details/79cd1d5e-d2c2-425a-844b-0a0535b951e6", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            imagesController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), imagesController);
            utilsMock.Setup(a => a.GetCurrentLanguage(imagesController)).Returns(2);

            // Act & Assert
            imagesController
            .WithCallTo(b => b.Details(imageObjectMock.TravelObjectId))
                .ShouldRenderDefaultView()
                .WithModel<ImageOutputViewModel>(viewModel =>
                {
                    Assert.AreEqual(imageTranslationalInfoMock.Description, viewModel.Description);
                    Assert.AreEqual(imageObjectMock.CreatedOn, viewModel.CreatedOn);
                });
        }

        [TestMethod]
        public void ReturnView_WhenValidGuidIsPassedAndLanuageIsEn()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var imageTranslationalInfoService = new Mock<IImageTranslationalInfoService>();
            var travelServiceMock = new Mock<ITravelService>();
            var utilsMock = new Mock<IUtils>();

            Guid imageId = Guid.NewGuid();

            Image imageObjectMock = new Image()
            {
                TravelObjectId = imageId,
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 09, 09)
            };

            ImageTranslationalInfo imageTranslationalInfoMock =
            new ImageTranslationalInfo()
            {
                TravelObjectId = imageObjectMock.TravelObjectId,
                Image = imageObjectMock,
                ImageTranslationalInfoId = Guid.NewGuid(),
                Description = "Test description photo",
                Language = Language.English
            };

            imageObjectMock.TranslatedInfoes.Add(imageTranslationalInfoMock);

            imageServiceMock.Setup(
                m => m.GetById((Guid?)imageObjectMock.TravelObjectId))
                .Returns(new Image()
                {
                    TravelObjectId = imageObjectMock.TravelObjectId,
                    CreatedOn = imageObjectMock.CreatedOn,
                    Content = imageObjectMock.Content,
                    TranslatedInfoes =
                    {
                         imageTranslationalInfoMock
                    }
                });

            ImagesController imagesController =
                 new ImagesController(
                imageServiceMock.Object,
                mountainsServiceMock.Object,
                imageTranslationalInfoService.Object,
                travelServiceMock.Object,
                utilsMock.Object);

            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/en/Images/Details/79cd1d5e-d2c2-425a-844b-0a0535b951e6", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            imagesController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), imagesController);
            utilsMock.Setup(a => a.GetCurrentLanguage(imagesController)).Returns(1);

            // Act & Assert
            imagesController
            .WithCallTo(b => b.Details(imageObjectMock.TravelObjectId))
                .ShouldRenderDefaultView()
                .WithModel<ImageOutputViewModel>(viewModel =>
                {
                    Assert.AreEqual(imageTranslationalInfoMock.Description, viewModel.Description);
                    Assert.AreEqual(imageObjectMock.CreatedOn, new DateTime(2017, 09, 09));
                });
        }

        [TestMethod]
        public void ReturnView_WhenTravelGuidNotMatchWithExistingTravel()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var imageTranslationalInfoService = new Mock<IImageTranslationalInfoService>();
            var travelServiceMock = new Mock<ITravelService>();
            var utilsMock = new Mock<IUtils>();

            Guid id = Guid.NewGuid();

            imageServiceMock.Setup(m => m.GetById((Guid?)null)).Returns((Image)null);

            ImagesController imagesController = new ImagesController(
                imageServiceMock.Object,
                mountainsServiceMock.Object,
                imageTranslationalInfoService.Object,
                travelServiceMock.Object,
                utilsMock.Object);

            // Act and Assert
            imagesController.WithCallTo(
                b => b.Details(id))
                  .ShouldRedirectTo<ImagesController>(typeof(ImagesController)
                  .GetMethod("SearchImages"));

        }
    }
}
