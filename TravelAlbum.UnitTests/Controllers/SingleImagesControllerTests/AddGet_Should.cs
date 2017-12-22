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
            var mountainsServiceMock = new Mock<IMountainsService>();
            var singleImageTranslationalInfoServiceMock = new Mock<ISingleImageTranslationalInfoService>();

            ImagesController singleImagesController = new ImagesController(singleImageServiceMock.Object, mountainsServiceMock.Object, singleImageTranslationalInfoServiceMock.Object);

            singleImagesController.WithCallTo(
                b => b.Add()).ShouldRenderDefaultView();
        }
    }
}
