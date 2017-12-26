using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.Data;
using TravelAlbum.DataServices;
using TravelAlbum.Models;

namespace TravelAlbum.UnitTests.DataServices.ImageServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenBothParametersAreNotNull()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<Image>>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            // Act
            ImageService imageService = new ImageService(wrapperMock.Object, dbContextMock.Object);

            // Assert
            Assert.IsNotNull(imageService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenImageSetWrapperIsNull()
        {
            // Arrange
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            // Act & Assert
            ImageService imageService = new ImageService(null, dbContextMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenDbContextIsNull()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<Image>>();

            // Act & Assert
            ImageService imageService = new ImageService(wrapperMock.Object, null);
        }
    }
}
