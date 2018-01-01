using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;
using TravelAlbum.Web.Utils;

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
            var imageServiceMock = new Mock<IImageService>();
            var utilsMock = new Mock<IUtils>();

            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, imageServiceMock.Object, utilsMock.Object);

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
            var utilsMock = new Mock<IUtils>();

            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, null, utilsMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterTravelTranslationalInfoIsNull()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var imageServiceMock = new Mock<IImageService>();
            var utilsMock = new Mock<IUtils>();

            // Act
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, null, imageServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterTravelServiceIsNull()
        {
            // Arrange
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();
            var imageServiceMock = new Mock<IImageService>();
            var utilsMock = new Mock<IUtils>();

            // Act
            TravelsController travelsController = new TravelsController(null, travelTranslationalInfoServiceMock.Object, imageServiceMock.Object, utilsMock.Object);

            // Assert
            Assert.IsNotNull(travelsController);
        }
    }
}
