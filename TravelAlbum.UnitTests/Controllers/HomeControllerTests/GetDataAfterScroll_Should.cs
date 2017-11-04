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

namespace TravelAlbum.UnitTests.Controllers.HomeControllerTests
{
    [TestClass]
    public class GetDataAfterScroll_Should
    {
        [TestMethod]
        public void ReturnsSingleImages_WhenEnglishLanguageIsPassed()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();

            SingleImage singleImage1 = new SingleImage()
            {
                SingleImageId = Guid.NewGuid(),
                Content = new byte[2] {1, 2},
                CreatedOn = new DateTime(2016, 9, 9)
            };

            SingleImage singleImage2 = new SingleImage()
            {
                SingleImageId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };

            SingleImage singleImage3 = new SingleImage()
            {
                SingleImageId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2015, 2, 2)
            };


            SingleImageTranslationalInfo singleImageTranslationalInfo1en = new SingleImageTranslationalInfo()
            {
                Description = "Test Description Single Image 1",
                SingleImage = singleImage1,
                SingleImageId = singleImage1.SingleImageId,
                Language = Language.English
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo2en = new SingleImageTranslationalInfo()
            {
                Description = "Test Description Single Image 2",
                SingleImage = singleImage2,
                SingleImageId = singleImage2.SingleImageId,
                Language = Language.English
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo3en = new SingleImageTranslationalInfo()
            {
                Description = "Test Description Single Image 3",
                SingleImage = singleImage3,
                SingleImageId = singleImage3.SingleImageId,
                Language = Language.English
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo1bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 1",
                SingleImage = singleImage1,
                SingleImageId = singleImage1.SingleImageId,
                Language = Language.Bulgarian
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo2bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 2",
                SingleImage = singleImage2,
                SingleImageId = singleImage2.SingleImageId,
                Language = Language.Bulgarian
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo3bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 3",
                SingleImage = singleImage3,
                SingleImageId = singleImage3.SingleImageId,
                Language = Language.Bulgarian
            };


            singleImage1.TranslatedInfoes.Add(singleImageTranslationalInfo1en);
            singleImage1.TranslatedInfoes.Add(singleImageTranslationalInfo1bg);

            singleImage2.TranslatedInfoes.Add(singleImageTranslationalInfo2en);
            singleImage2.TranslatedInfoes.Add(singleImageTranslationalInfo2bg);

            singleImage3.TranslatedInfoes.Add(singleImageTranslationalInfo3en);
            singleImage3.TranslatedInfoes.Add(singleImageTranslationalInfo3bg);

            List<SingleImage> singleImages = new List<SingleImage>() { singleImage1, singleImage2, singleImage3 };

            singleImageServiceMock.Setup(
                m => m.GetLatesSingleImages(0))
                .Returns(singleImages);
            // Act
            HomeController homeController =
                 new HomeController(singleImageServiceMock.Object);

            JsonResult result = homeController.GetSingleImagesOnScroll("/en", 0);

            dynamic items = result.Data;

            // Assert
            Assert.AreEqual("Test Description Single Image 1", items[0].Description);
            Assert.AreEqual("Test Description Single Image 2", items[1].Description);
            Assert.AreEqual(new DateTime(2016, 9, 9), items[0].CreatedOn);
            Assert.AreEqual(new DateTime(2017, 10, 10), items[1].CreatedOn);
        }

        [TestMethod]
        public void ReturnsSingleImages_WhenEnglishBulgarianIsPassed()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();

            SingleImage singleImage1 = new SingleImage()
            {
                SingleImageId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2016, 9, 9)
            };

            SingleImage singleImage2 = new SingleImage()
            {
                SingleImageId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };   

            SingleImageTranslationalInfo singleImageTranslationalInfo1bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 1",
                SingleImage = singleImage1,
                SingleImageId = singleImage1.SingleImageId,
                Language = Language.Bulgarian
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo2bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 2",
                SingleImage = singleImage2,
                SingleImageId = singleImage2.SingleImageId,
                Language = Language.Bulgarian
            };

            singleImage1.TranslatedInfoes.Add(singleImageTranslationalInfo1bg);
            singleImage2.TranslatedInfoes.Add(singleImageTranslationalInfo2bg);

            List<SingleImage> singleImages = new List<SingleImage>() { singleImage1, singleImage2 };

            singleImageServiceMock.Setup(
                m => m.GetLatesSingleImages(0))
                .Returns(singleImages);
            // Act
            HomeController homeController =
                 new HomeController(singleImageServiceMock.Object);

            JsonResult result = homeController.GetSingleImagesOnScroll("/bg", 0);

            dynamic items = result.Data;

            // Assert
            Assert.AreEqual("Тест описание Single Image 1", items[0].Description);
            Assert.AreEqual("Тест описание Single Image 2", items[1].Description);
            Assert.AreEqual(new DateTime(2016, 9, 9), items[0].CreatedOn);
            Assert.AreEqual(new DateTime(2017, 10, 10), items[1].CreatedOn);
        }
    }
}
