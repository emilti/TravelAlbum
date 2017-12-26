using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.HomeControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenParameterIsNotNull()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();

            // Act
            HomeController homeController = new HomeController(imageServiceMock.Object);

            // Assert
            Assert.IsNotNull(homeController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterIsNull()
        {
            // Act
            HomeController homeController = new HomeController(null);

            // Assert
            Assert.IsNotNull(homeController);
        }
    }
}
