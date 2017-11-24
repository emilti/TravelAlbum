using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
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

            List<SingleImageOutputViewModel> images = new List<SingleImageOutputViewModel>();
            if (orderedSingleImages != null && orderedSingleImages.Count() > 0)
            {
                foreach (var singleImage in orderedSingleImages)
                {
                    SingleImageOutputViewModel singleImageOutputViewModel = new SingleImageOutputViewModel();
                    // string query = Request.Url.PathAndQuery;

                    string description = String.Empty;
                    //TODO: fix en and bg string conditions
                    if (!(url.Contains("/en")))
                    {
                        description = SetDescription(singleImage, Language.Bulgarian);
                    }
                    else
                    {
                        description = SetDescription(singleImage, Language.English);
                    }

                    string imageData = Convert.ToBase64String(singleImage.Content);

                    singleImageOutputViewModel.SingleImageId = singleImage.TravelObjectId;
                    singleImageOutputViewModel.Description = description;
                    singleImageOutputViewModel.SingleImageData = imageData;
                    singleImageOutputViewModel.CreatedOn = singleImage.CreatedOn;                    
                    images.Add(singleImageOutputViewModel);
                }
               
                return Json(images, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(images, JsonRequestBehavior.AllowGet);
            }
        }

        private string SetDescription(SingleImage singleImage, Language language)
        {
            IEnumerable<SingleImageTranslationalInfo> infos =
                                    singleImage.TranslatedInfoes.Where(x => x.Language == language).ToList();
            SingleImageTranslationalInfo singleImageTranslationalInfo = infos.First();
            return singleImageTranslationalInfo.Description;
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