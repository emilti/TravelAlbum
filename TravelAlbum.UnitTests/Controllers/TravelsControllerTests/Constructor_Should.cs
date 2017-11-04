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
            var travelImageServiceMock = new Mock<ITravelImageService>();
            
            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, travelImageServiceMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReturnsAnInstance_WhenParameterTravelImageIsNull()
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
        public void ReturnsAnInstance_WhenParameterTravelTranslationalInfoIsNull()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var travelImageServiceMock = new Mock<ITravelImageService>();

            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, null, travelImageServiceMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReturnsAnInstance_WhenParameterTravelServiceIsNull()
        {
            // Arrange
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();
            var travelImageServiceMock = new Mock<ITravelImageService>();

            // Act
            TravelsController travelsController = new TravelsController(null, travelTranslationalInfoServiceMock.Object, travelImageServiceMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }
    }
}
