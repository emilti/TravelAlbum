using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.ImagesControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var imageTranslationalInfoServiceMock = new Mock<IImageTranslationalInfoService>();
           
            // Act
            ImagesController imageController = new ImagesController(imageServiceMock.Object, mountainsServiceMock.Object, imageTranslationalInfoServiceMock.Object);

            // Assert
            Assert.IsNotNull(imageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterImageServiceIsNull()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();

            // Act
            ImagesController imageController = new ImagesController(imageServiceMock.Object, mountainsServiceMock.Object, null);

            // Assert
            Assert.IsNotNull(imageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterImageTranslationalInfoIsNull()
        {
            var imageTranslationalInfoServiceMock = new Mock<IImageTranslationalInfoService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            // Act
            ImagesController imageController = new ImagesController(null, mountainsServiceMock.Object, imageTranslationalInfoServiceMock.Object);

            // Assert
            Assert.IsNotNull(imageController);
        }        
    }
}
