using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Data;
using TravelAlbum.DataServices;
using TravelAlbum.Models;

namespace TravelAlbum.UnitTests.DataServices.ImageServiceTests
{
    [TestClass]
    public class GetLatestImages_Should
    {
        [TestMethod]
        public void ReturnFirstTwoElementsInCollection_When0PageIndexIsPassed()
        {
            var wrapperMock = new Mock<IEfDbSetWrapper<Image>>();
           //  var ImageServiceMock = new Mock<IImageService>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();


            Image image = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = new DateTime(2017, 10, 10),
                Content = new byte[2] { 1, 2 }
            };

            Image image2 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = new DateTime(2016, 9, 9),
                Content = new byte[2] { 1, 3 }
            };

            Image image3 = new Image()
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = new DateTime(2016, 9, 9),
                Content = new byte[3] { 1, 2, 3 }
            };

            ImageTranslationalInfo imageTranslationalInfo1 = new ImageTranslationalInfo()
            {
                Description = "Test Description",
                Image = image,
                TravelObjectId = image.TravelObjectId,
                Language = Language.English
            };

            image.TranslatedInfoes.Add(imageTranslationalInfo1);
            
            var images = new List<Image>()
            {
                image,
                image2,
                image3
            };

            wrapperMock.Setup(m => m.All).Returns(images.AsQueryable);

            ImageService imageService = new ImageService(wrapperMock.Object, dbContextMock.Object);
            var result = imageService.GetLatesImages(0);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(new DateTime(2017, 10, 10), result.First().CreatedOn);
            Assert.AreEqual(new DateTime(2016, 9, 9), result.ElementAt(1).CreatedOn);
            Assert.AreEqual("Test Description", result.ElementAt(0).TranslatedInfoes.ElementAt(0).Description);

            var resultWithPageIndex1 = imageService.GetLatesImages(1);
            Assert.AreEqual(0, resultWithPageIndex1.Count());
        }
    }
}
