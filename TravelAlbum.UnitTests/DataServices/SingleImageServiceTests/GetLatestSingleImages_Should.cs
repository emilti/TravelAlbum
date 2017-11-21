using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Data;
using TravelAlbum.DataServices;
using TravelAlbum.Models;

namespace TravelAlbum.UnitTests.DataServices.SingleImageServiceTests
{
    [TestClass]
    public class GetLatestSingleImages_Should
    {
        [TestMethod]
        public void ReturnFirstTwoElementsInCollection_When0PageIndexIsPassed()
        {
            var wrapperMock = new Mock<IEfDbSetWrapper<SingleImage>>();
           //  var singleImageServiceMock = new Mock<ISingleImageService>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();


            SingleImage singleImage = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = new DateTime(2017, 10, 10),
                Content = new byte[2] { 1, 2 }
            };

            SingleImage singleImage2 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = new DateTime(2016, 9, 9),
                Content = new byte[2] { 1, 3 }
            };

            SingleImage singleImage3 = new SingleImage()
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = new DateTime(2016, 9, 9),
                Content = new byte[3] { 1, 2, 3 }
            };

            SingleImageTranslationalInfo singleImageTranslationalInfo1 = new SingleImageTranslationalInfo()
            {
                Description = "Test Description",
                SingleImage = singleImage,
                TravelObjectId = singleImage.TravelObjectId,
                Language = Language.English
            };

            singleImage.TranslatedInfoes.Add(singleImageTranslationalInfo1);
            
            var singleImages = new List<SingleImage>()
            {
                singleImage,
                singleImage2,
                singleImage3
            };

            wrapperMock.Setup(m => m.All).Returns(singleImages.AsQueryable);

            SingleImageService singleImageService = new SingleImageService(wrapperMock.Object, dbContextMock.Object);
            var result = singleImageService.GetLatesSingleImages(0);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(new DateTime(2017, 10, 10), result.First().CreatedOn);
            Assert.AreEqual(new DateTime(2016, 9, 9), result.ElementAt(1).CreatedOn);
            Assert.AreEqual("Test Description", result.ElementAt(0).TranslatedInfoes.ElementAt(0).Description);

            var resultWithPageIndex1 = singleImageService.GetLatesSingleImages(1);
            Assert.AreEqual(1, resultWithPageIndex1.Count());
        }
    }
}
