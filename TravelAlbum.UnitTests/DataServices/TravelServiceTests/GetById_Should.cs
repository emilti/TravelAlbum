using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices;
using TravelAlbum.Models;

namespace TravelAlbum.UnitTests.DataServices.TravelServiceTests
{
    [TestClass]
    public class GetById_Should
    {
        [TestMethod]
        public void ReturnModel_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<Travel>>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();
            Guid? travelId = Guid.NewGuid();

            wrapperMock.Setup(m => m.GetById(travelId.Value)).Returns(new Travel() { TravelId = travelId.Value, CreatedOn = DateTime.Now });

            TravelService travelService = new TravelService(wrapperMock.Object, dbContextMock.Object);

            // Act
            Travel travel = travelService.GetById(travelId);

            // Assert
            Assert.IsNotNull(travel);
        }

        [TestMethod]
        public void ReturnNull_WhenIdIsNull()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<Travel>>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            TravelService travelService = new TravelService(wrapperMock.Object, dbContextMock.Object);

            // Act
            Travel travel = travelService.GetById(null);

            // Assert
            Assert.IsNull(travel);
        }

        [TestMethod]
        public void ReturnNull_WhenThereIsNoModelWithThePassedId()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<Travel>>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();
            Guid? travelId = Guid.NewGuid();

            wrapperMock.Setup(m => m.GetById(travelId.Value)).Returns((Travel)null);

            TravelService travelService = new TravelService(wrapperMock.Object, dbContextMock.Object);

            // Act
            Travel travel = travelService.GetById(travelId);

            // Assert
            Assert.IsNull(travel);
        }
    }
}
