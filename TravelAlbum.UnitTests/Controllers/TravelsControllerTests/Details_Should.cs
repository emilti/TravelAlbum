﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestStack.FluentMVCTesting;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Controllers;
using TravelAlbum.Web.Models.TravelModels;

namespace TravelAlbum.UnitTests.Controllers.TravelsControllerTests
{
    [TestClass]
    public class Details_Should
    {
        [TestMethod]
        public void ReturnView_WhenValidGuidIsPassedAndLanuageIsBg()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var imageServiceMock = new Mock<IImageService>();
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();

            Guid travelId = Guid.NewGuid();

            Travel travelObjectMock = new Travel()
            {
                TravelObjectId = travelId,
                CreatedOn = DateTime.Now
            };

            
            TravelTranslationalInfo travelTranslationalInfoMock =
            new TravelTranslationalInfo()
            {
                TravelObjectId = travelObjectMock.TravelObjectId,
                TravelObject = travelObjectMock,
                Title = "Тест заглавие",
                Description = "Тест описание",
                Language = Language.Bulgarian
            };

            Image imageMock = new Image()
            {
                TravelObjectId = travelObjectMock.TravelObjectId,
                Travel = travelObjectMock,
                Content = new byte[] {1,2}
            };

            travelObjectMock.TranslatedTravels.Add(travelTranslationalInfoMock);
            travelObjectMock.Images.Add(imageMock);

            travelServiceMock.Setup(
                m => m.GetById((Guid?)travelObjectMock.TravelObjectId))
                .Returns(new Travel()
                {                    
                    TravelObjectId = travelObjectMock.TravelObjectId,
                    CreatedOn = DateTime.Now,
                    TranslatedTravels =
                    {
                        travelTranslationalInfoMock
                    },
                    Images =
                    {
                        imageMock
                    },
                    StartDate = null,
                    EndDate = null                    
                });

            // wrapperMock.Setup(m => m.GetById(travelId.Value)).Returns(new Travel() { TravelId = travelId.Value, CreatedOn = DateTime.Now });
            TravelsController travelsController =
                 new TravelsController(
                travelServiceMock.Object,
                travelTranslationalInfoServiceMock.Object,
                imageServiceMock.Object);
                      
            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/bg/Travels/Details/79cd1d5e-d2c2-425a-844b-0a0535b951e6", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            travelsController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), travelsController);
            
            // Act & Assert
            travelsController
                .WithCallTo(b => b.Details(travelObjectMock.TravelObjectId))
                .ShouldRenderDefaultView()
                .WithModel<DetailsTravelOutputViewModel>(viewModel =>
                {
                    Assert.AreEqual(travelTranslationalInfoMock.Title, viewModel.Title);
                    Assert.AreEqual(travelTranslationalInfoMock.Description, viewModel.Description);               
                });

        }

        [TestMethod]
        public void ReturnView_WhenValidGuidIsPassedAndLanuageIsEn()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var imageServiceMock = new Mock<IImageService>();
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();

            Guid travelId = Guid.NewGuid();

            Travel travelObjectMock = new Travel()
            {
                TravelObjectId = travelId,
                CreatedOn = DateTime.Now
            };


            TravelTranslationalInfo travelTranslationalInfoMock =
            new TravelTranslationalInfo()
            {
                TravelObjectId = travelObjectMock.TravelObjectId,
                TravelObject = travelObjectMock,
                Title = "Test title",
                Description = "Test description",
                Language = Language.English
            };

            Image imageMock = new Image()
            {
                TravelObjectId = travelObjectMock.TravelObjectId,
                Travel = travelObjectMock,
                Content = new byte[] { 1, 2 }
            };

            travelObjectMock.TranslatedTravels.Add(travelTranslationalInfoMock);
            travelObjectMock.Images.Add(imageMock);

            travelServiceMock.Setup(
                m => m.GetById((Guid?)travelObjectMock.TravelObjectId))
                .Returns(new Travel()
                {
                    TravelObjectId = travelObjectMock.TravelObjectId,
                    CreatedOn = DateTime.Now,
                    TranslatedTravels =
                    {
                        travelTranslationalInfoMock
                    },
                    Images =
                    {
                        imageMock
                    },
                    StartDate = null,
                    EndDate = null
                });

            // wrapperMock.Setup(m => m.GetById(travelId.Value)).Returns(new Travel() { TravelId = travelId.Value, CreatedOn = DateTime.Now });
            TravelsController travelsController =
                 new TravelsController(
                travelServiceMock.Object,
                travelTranslationalInfoServiceMock.Object,
                imageServiceMock.Object);

            HttpRequest httpRequest = new HttpRequest("", "http://localhost:56342/en/Travels/Details/79cd1d5e-d2c2-425a-844b-0a0535b951e6", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponse = new HttpResponse(stringWriter);
            HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            travelsController.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), travelsController);

            // Act & Assert
            travelsController
                .WithCallTo(b => b.Details(travelObjectMock.TravelObjectId))
                .ShouldRenderDefaultView()
                .WithModel<DetailsTravelOutputViewModel>(viewModel =>
                {
                    Assert.AreEqual(travelTranslationalInfoMock.Title, viewModel.Title);
                    Assert.AreEqual(travelTranslationalInfoMock.Description, viewModel.Description);
                });
        }

        [TestMethod]
        public void ReturnView_WhenTravelGuidNotMatchWithExistingTravel()
        {
            // Arrange
            var travelServiceMock = new Mock<ITravelService>();
            var imageServiceMock = new Mock<IImageService>();
            var travelTranslationalInfoServiceMock = new Mock<ITravelTranslationalInfoService>();

            Guid id = Guid.NewGuid();
            
            travelServiceMock.Setup(m => m.GetById((Guid?)null)).Returns((Travel)null);
             
            TravelsController travelsController = new TravelsController(travelServiceMock.Object, travelTranslationalInfoServiceMock.Object, imageServiceMock.Object);
                 
            // Act and Assert
            travelsController.WithCallTo(
                b => b.Details(id))
                  .ShouldRedirectTo<HomeController>(typeof(HomeController)
                  .GetMethod("Index")); 
        }
    }
}
