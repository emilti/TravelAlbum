using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.HomeControllerTests
{
    [TestClass]
    public class GetDataOnScroll_Should
    {
        [TestMethod]
        public void ReturnsSingleImages_WhenEnglishLanguageIsPassed()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();

            SingleImage singleImage1 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] {1, 2},
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2016, 9, 9)
            };

            SingleImage singleImage2 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };

            SingleImage singleImage3 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2015, 2, 2)
            };


            SingleImageTranslationalInfo singleImageTranslationalInfo1en = new SingleImageTranslationalInfo()
            {
                Description = "Test Description Single Image 1",
                SingleImage = singleImage1,
                TravelObjectId = singleImage1.TravelObjectId,
                Language = Language.English
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo2en = new SingleImageTranslationalInfo()
            {
                Description = "Test Description Single Image 2",
                SingleImage = singleImage2,
                TravelObjectId = singleImage2.TravelObjectId,
                Language = Language.English
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo3en = new SingleImageTranslationalInfo()
            {
                Description = "Test Description Single Image 3",
                SingleImage = singleImage3,
                TravelObjectId = singleImage3.TravelObjectId,
                Language = Language.English
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo1bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 1",
                SingleImage = singleImage1,
                TravelObjectId = singleImage1.TravelObjectId,
                Language = Language.Bulgarian
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo2bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 2",
                SingleImage = singleImage2,
                TravelObjectId = singleImage2.TravelObjectId,
                Language = Language.Bulgarian
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo3bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 3",
                SingleImage = singleImage3,
                TravelObjectId = singleImage3.TravelObjectId,
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
            Assert.AreEqual(new DateTime(2016, 9, 9), items[0].CreatedOn);
            Assert.AreEqual(new DateTime(2017, 10, 10), items[1].CreatedOn);
        }

        [TestMethod]
        public void ReturnsSingleImages_WhenBulgarianIsPassed()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();

            SingleImage singleImage1 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2016, 9, 9)
            };

            SingleImage singleImage2 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };   

            SingleImageTranslationalInfo singleImageTranslationalInfo1bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 1",
                SingleImage = singleImage1,
                TravelObjectId = singleImage1.TravelObjectId,
                Language = Language.Bulgarian
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo2bg = new SingleImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 2",
                SingleImage = singleImage2,
                TravelObjectId = singleImage2.TravelObjectId,
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
           
            Assert.AreEqual(new DateTime(2016, 9, 9), items[0].CreatedOn);
            Assert.AreEqual(new DateTime(2017, 10, 10), items[1].CreatedOn);
        }

        [TestMethod]
        public void ReturnsSingleImages_WhenEnglishBulgarianIsPassed()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();

            IEnumerable<SingleImage> singleImages = new List<SingleImage>();           
            singleImageServiceMock.Setup(
                m => m.GetLatesSingleImages(0))
                .Returns(singleImages);
            // Act
            HomeController homeController =
                 new HomeController(singleImageServiceMock.Object);

            JsonResult result = homeController.GetSingleImagesOnScroll("/bg", 0);

            dynamic items = result.Data;

            // Assert
            Assert.AreEqual(0, items.Count);
        }
    }
}
