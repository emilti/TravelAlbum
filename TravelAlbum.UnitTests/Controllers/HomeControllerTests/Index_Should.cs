using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;

namespace TravelAlbum.UnitTests.Controllers.HomeControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestMethod]
        public void ReturnView_CheckDefaultIndexView()
        {
            var imageServiceMock = new Mock<IImageService>();
           
            HomeController homeController = new HomeController(imageServiceMock.Object);

            homeController.WithCallTo(
                b => b.Index(0)).ShouldRenderDefaultView();
        }
    }
}
