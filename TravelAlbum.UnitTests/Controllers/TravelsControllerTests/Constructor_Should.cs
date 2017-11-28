using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.TravelsControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();
            var singleImageServiceMock = new Mock<ISingleImageService>();
            
            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, singleImageServiceMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterTravelImageIsNull()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();

            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, null);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterTravelTranslationalInfoIsNull()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var singleImageServiceMock = new Mock<ISingleImageService>();

            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, null, singleImageServiceMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterTravelServiceIsNull()
        {
            // Arrange
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();
            var singleImageServiceMock = new Mock<ISingleImageService>();

            // Act
            TravelsController travelsController = new TravelsController(null, travelTranslationalInfoServiceMock.Object, singleImageServiceMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }
    }
}
