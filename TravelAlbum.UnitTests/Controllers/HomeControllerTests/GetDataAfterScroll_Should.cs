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
        public void ReturnsImages_WhenEnglishLanguageIsPassed()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();

            Image image1 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] {1, 2},
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2016, 9, 9)
            };

            Image image2 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };

            Image image3 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2015, 2, 2)
            };


            ImageTranslationalInfo imageTranslationalInfo1en = new ImageTranslationalInfo()
            {
                Description = "Test Description Single Image 1",
                Image = image1,
                TravelObjectId = image1.TravelObjectId,
                Language = Language.English
            };

            ImageTranslationalInfo imageTranslationalInfo2en = new ImageTranslationalInfo()
            {
                Description = "Test Description Single Image 2",
                Image = image2,
                TravelObjectId = image2.TravelObjectId,
                Language = Language.English
            };

            ImageTranslationalInfo imageTranslationalInfo3en = new ImageTranslationalInfo()
            {
                Description = "Test Description Single Image 3",
                Image = image3,
                TravelObjectId = image3.TravelObjectId,
                Language = Language.English
            };

            ImageTranslationalInfo imageTranslationalInfo1bg = new ImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 1",
                Image = image1,
                TravelObjectId = image1.TravelObjectId,
                Language = Language.Bulgarian
            };

            ImageTranslationalInfo imageTranslationalInfo2bg = new ImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 2",
                Image = image2,
                TravelObjectId = image2.TravelObjectId,
                Language = Language.Bulgarian
            };

            ImageTranslationalInfo imageTranslationalInfo3bg = new ImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 3",
                Image = image3,
                TravelObjectId = image3.TravelObjectId,
                Language = Language.Bulgarian
            };


            image1.TranslatedInfoes.Add(imageTranslationalInfo1en);
            image1.TranslatedInfoes.Add(imageTranslationalInfo1bg);

            image2.TranslatedInfoes.Add(imageTranslationalInfo2en);
            image2.TranslatedInfoes.Add(imageTranslationalInfo2bg);

            image3.TranslatedInfoes.Add(imageTranslationalInfo3en);
            image3.TranslatedInfoes.Add(imageTranslationalInfo3bg);

            List<Image> images = new List<Image>() { image1, image2, image3 };

            imageServiceMock.Setup(
                m => m.GetLatesImages(0))
                .Returns(images);
            // Act
            HomeController homeController =
                 new HomeController(imageServiceMock.Object);

            JsonResult result = homeController.GetImagesOnScroll("/en", 0);

            dynamic items = result.Data;

            // Assert           
            Assert.AreEqual(new DateTime(2016, 9, 9), items[0].CreatedOn);
            Assert.AreEqual(new DateTime(2017, 10, 10), items[1].CreatedOn);
        }

        [TestMethod]
        public void ReturnsImages_WhenBulgarianIsPassed()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();

            Image image1 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2016, 9, 9)
            };

            Image image2 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                Content = new byte[2] { 1, 2 },
                PreviewContent = new byte[2] { 1, 2 },
                CreatedOn = new DateTime(2017, 10, 10)
            };   

            ImageTranslationalInfo imageTranslationalInfo1bg = new ImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 1",
                Image = image1,
                TravelObjectId = image1.TravelObjectId,
                Language = Language.Bulgarian
            };

            ImageTranslationalInfo imageTranslationalInfo2bg = new ImageTranslationalInfo()
            {
                Description = "Тест описание Single Image 2",
                Image = image2,
                TravelObjectId = image2.TravelObjectId,
                Language = Language.Bulgarian
            };

            image1.TranslatedInfoes.Add(imageTranslationalInfo1bg);
            image2.TranslatedInfoes.Add(imageTranslationalInfo2bg);

            List<Image> images = new List<Image>() { image1, image2 };

            imageServiceMock.Setup(
                m => m.GetLatesImages(0))
                .Returns(images);
            // Act
            HomeController homeController =
                 new HomeController(imageServiceMock.Object);

            JsonResult result = homeController.GetImagesOnScroll("/bg", 0);

            dynamic items = result.Data;
                        // Assert
           
            Assert.AreEqual(new DateTime(2016, 9, 9), items[0].CreatedOn);
            Assert.AreEqual(new DateTime(2017, 10, 10), items[1].CreatedOn);
        }

        [TestMethod]
        public void ReturnsImages_WhenEnglishBulgarianIsPassed()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();

            IEnumerable<Image> images = new List<Image>();           
            imageServiceMock.Setup(
                m => m.GetLatesImages(0))
                .Returns(images);
            // Act
            HomeController homeController =
                 new HomeController(imageServiceMock.Object);

            JsonResult result = homeController.GetImagesOnScroll("/bg", 0);

            dynamic items = result.Data;

            // Assert
            Assert.AreEqual(0, items.Count);
        }
    }
}
