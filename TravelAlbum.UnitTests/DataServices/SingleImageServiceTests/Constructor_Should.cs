using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.Data;
using TravelAlbum.DataServices;
using TravelAlbum.Models;

namespace TravelAlbum.UnitTests.DataServices.SingleImageServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenBothParametersAreNotNull()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<SingleImage>>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            // Act
            SingleImageService singleImageService = new SingleImageService(wrapperMock.Object, dbContextMock.Object);

            // Assert
            Assert.IsNotNull(singleImageService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowException_WhenSingleImageSetWrapperIsNull()
        {
            // Arrange
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            // Act & Assert
            SingleImageService singleImageService = new SingleImageService(null, dbContextMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowException_WhenDbContextIsNull()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<SingleImage>>();

            // Act & Assert
            SingleImageService singleImageService = new SingleImageService(wrapperMock.Object, null);
        }
    }
}
