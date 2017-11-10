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
            var singleImageTranslationalInfoServiceMock = new Mock<ISingleImageTranslationalInfoService>();
           
            // Act
            SingleImagesController singleImageController = new SingleImagesController(singleImageServiceMock.Object, singleImageTranslationalInfoServiceMock.Object);

            // Assert
            Assert.IsNotNull(singleImageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterSingleImageServiceIsNull()
        {
            // Arrange
            var singleImageServiceMock = new Mock<ISingleImageService>();

            // Act
            SingleImagesController singleImageController = new SingleImagesController(singleImageServiceMock.Object, null);

            // Assert
            Assert.IsNotNull(singleImageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterSingleImageTranslationalInfoIsNull()
        {
            var singleImageTranslationalInfoServiceMock = new Mock<ISingleImageTranslationalInfoService>();

            // Act
            SingleImagesController singleImageController = new SingleImagesController(null, singleImageTranslationalInfoServiceMock.Object);

            // Assert
            Assert.IsNotNull(singleImageController);
        }        
    }
}
