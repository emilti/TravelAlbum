using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Models.SingleImageModels;
using TravelAlbum.Web.Models.TravelModels;

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
        public ActionResult Index()
        {
            var orderedSingleImages = this.singleImageService.GetLatesSingleImages();

            LatestSingleImagesOutputModel latestSingleImagesOutputModel = new LatestSingleImagesOutputModel();
            if (orderedSingleImages != null && orderedSingleImages.Count() > 0)
            {
                foreach (var singleImage in orderedSingleImages)
                {
                    SingleImageOutputViewModel singleImageOutputViewModel = new SingleImageOutputViewModel();
                    string query = Request.Url.PathAndQuery;

                    string description = String.Empty;
                    if (!(query.Contains("/en/")))
                    {
                        description = SetDescription(singleImage, Language.Bulgarian);
                    }
                    else
                    {
                        description = SetDescription(singleImage, Language.English);
                    }

                    string imageData = Convert.ToBase64String(singleImage.Content);

                    singleImageOutputViewModel.Description = description;
                    singleImageOutputViewModel.SingleImageData = imageData;
                    singleImageOutputViewModel.CreatodOn = singleImage.CreatedOn;
                    latestSingleImagesOutputModel.singleImages.Add(singleImageOutputViewModel);
                }

                return View(latestSingleImagesOutputModel);
            }
            else
            {
                return View(latestSingleImagesOutputModel);
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