using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.ImagesControllerTests
{
    [TestClass]
    public class AddGet_Should
    {
        [TestMethod]
        public void ReturnView_CheckView()
        {
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var imageTranslationalInfoServiceMock = new Mock<IImageTranslationalInfoService>();
            

            ImagesController imagesController = new ImagesController(imageServiceMock.Object, mountainsServiceMock.Object, imageTranslationalInfoServiceMock.Object);

            Mountain rila = new Mountain()
            {
                MountainId = Guid.NewGuid(),
                Name = "Rila"
            };

            Mountain vitosha = new Mountain()
            {
                MountainId = Guid.NewGuid(),
                Name = "Vitosha"
            };

            Mountain pirin = new Mountain()
            {
                MountainId = Guid.NewGuid(),
                Name = "Pirin"
            };

            MountainTranslationalInfo vitoshaBg = new MountainTranslationalInfo()
            {
                MountainTranslationalInfoId = Guid.NewGuid(),
                Mountain = vitosha,
                MountainId = vitosha.MountainId,
                Language = Language.Bulgarian,
                Name = "Витоша"
            };

            MountainTranslationalInfo vitoshaEn = new MountainTranslationalInfo()
            {
                MountainTranslationalInfoId = Guid.NewGuid(),
                Mountain = vitosha,
                MountainId = vitosha.MountainId,
                Language = Language.English,
                Name = "Vitosha"
            };

            MountainTranslationalInfo rilaBg = new MountainTranslationalInfo()
            {
                MountainTranslationalInfoId = Guid.NewGuid(),
                Mountain = rila,
                MountainId = rila.MountainId,
                Language = Language.Bulgarian,
                Name = "Рила"
            };

            MountainTranslationalInfo rilaEn = new MountainTranslationalInfo()
            {
                MountainTranslationalInfoId = Guid.NewGuid(),
                Mountain = rila,
                MountainId = rila.MountainId,
                Language = Language.English,
                Name = "Rila"
            };

            MountainTranslationalInfo pirinBg = new MountainTranslationalInfo()
            {
                MountainTranslationalInfoId = Guid.NewGuid(),
                Mountain = pirin,
                MountainId = pirin.MountainId,
                Language = Language.Bulgarian,
                Name = "Пирин"
            };

            MountainTranslationalInfo pirinEn = new MountainTranslationalInfo()
            {
                MountainTranslationalInfoId = Guid.NewGuid(),
                Mountain = pirin,
                MountainId = pirin.MountainId,
                Language = Language.English,
                Name = "Pirin"
            };

            rila.TranslatedInfoes.Add(rilaBg);
            rila.TranslatedInfoes.Add(rilaEn);
            vitosha.TranslatedInfoes.Add(vitoshaBg);
            vitosha.TranslatedInfoes.Add(vitoshaEn);
            pirin.TranslatedInfoes.Add(pirinBg);
            pirin.TranslatedInfoes.Add(pirinEn);
            mountainsServiceMock.Setup(
                m => m.All())
                .Returns((new List<Mountain>() { rila, pirin, vitosha }));

            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/bg", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            imagesController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), imagesController);

            imagesController.WithCallTo(
                b => b.Add()).ShouldRenderDefaultView();
        }
    }
}
