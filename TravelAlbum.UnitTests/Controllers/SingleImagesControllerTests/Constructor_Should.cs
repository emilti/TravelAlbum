using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.SingleImagesControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var singleImageTranslationalInfoServiceMock = new Mock<ISingleImageTranslationalInfoService>();
           
            // Act
            SingleImagesController singleImageController = new SingleImagesController(singleImageServiceMock.Object, mountainsServiceMock.Object, singleImageTranslationalInfoServiceMock.Object);

            // Assert
            Assert.IsNotNull(singleImageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterSingleImageServiceIsNull()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();

            // Act
            SingleImagesController singleImageController = new SingleImagesController(singleImageServiceMock.Object, mountainsServiceMock.Object, null);

            // Assert
            Assert.IsNotNull(singleImageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterSingleImageTranslationalInfoIsNull()
        {
            var singleImageTranslationalInfoServiceMock = new Mock<ISingleImageTranslationalInfoService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            // Act
            SingleImagesController singleImageController = new SingleImagesController(null, mountainsServiceMock.Object, singleImageTranslationalInfoServiceMock.Object);

            // Assert
            Assert.IsNotNull(singleImageController);
        }        
    }
}
