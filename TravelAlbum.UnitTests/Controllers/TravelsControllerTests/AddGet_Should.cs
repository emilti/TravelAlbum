using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.TravelsControllerTests
{
    [TestClass]
    public class AddGet_Should
    {
        [TestMethod]
        public void ReturnView_CheckView()
        {
            var travelServiceMock = new Mock<ITravelService>();
            var travelImageServiceMock = new Mock<ITravelImageService>();
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();

            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, travelImageServiceMock.Object);

            travelsController.WithCallTo(
                b => b.Add()).ShouldRenderDefaultView();                  
        }
    }
}
