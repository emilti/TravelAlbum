using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.SingleImagesControllerTests
{
    [TestClass]
    public class AddGet_Should
    {
        [TestMethod]
        public void ReturnView_CheckView()
        {
            var singleImageServiceMock = new Mock<ISingleImageService>();
            var singleImageTranslationalInfoServiceMock = new Mock<ISingleImageTranslationalInfoService>();

            SingleImagesController singleImagesController = new SingleImagesController(singleImageServiceMock.Object, singleImageTranslationalInfoServiceMock.Object);

            singleImagesController.WithCallTo(
                b => b.Add()).ShouldRenderDefaultView();
        }
    }
}
