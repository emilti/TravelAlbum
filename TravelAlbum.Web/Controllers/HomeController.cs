using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Helpers;
using TravelAlbum.Web.Models.SingleImageModels;

namespace TravelAlbum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISingleImageService singleImageService;

        public HomeController(ISingleImageService singleImageService)
        {
            Guard.WhenArgument(singleImageService, "singleImageService").IsNull().Throw();
            this.singleImageService = singleImageService;
        }

        public ActionResult Index(int pageIndex = 0)
        {
            return View();
        }

        public JsonResult GetSingleImagesOnScroll(string url, int pageIndex = 0)
        {
            var orderedSingleImages = this.singleImageService.GetLatesSingleImages(pageIndex);

            List<SingleImagePreviewOutputViewModel> images = new List<SingleImagePreviewOutputViewModel>();
            if (orderedSingleImages != null && orderedSingleImages.Count() > 0)
            {
                foreach (var singleImage in orderedSingleImages)
                {
                    SingleImagePreviewOutputViewModel singleImagePreviewOutputViewModel = new SingleImagePreviewOutputViewModel();
                    // string query = Request.Url.PathAndQuery;
                    
                    
                    //TODO: fix en and bg string conditions
                    // if (!(url.Contains("/en")))
                    // {
                    //     description = SetDescription(singleImage, Language.Bulgarian);
                    // }
                    // else
                    // {
                    //     description = SetDescription(singleImage, Language.English);
                    // }

                    string imagePreviewData = Convert.ToBase64String(singleImage.PreviewContent);

                    singleImagePreviewOutputViewModel.SingleImageId = singleImage.TravelObjectId;                   
                    singleImagePreviewOutputViewModel.SingleImageData = imagePreviewData;
                    singleImagePreviewOutputViewModel.CreatedOn = singleImage.CreatedOn;                    
                    images.Add(singleImagePreviewOutputViewModel);
                }

                var jsonResult = Json(images, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                var jsonResult = Json(images, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }       

        public ActionResult About()
        {
            ViewBag.Message = "TEST!2Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}