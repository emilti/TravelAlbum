using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Helpers;
using TravelAlbum.Web.Models.ImageModels;


namespace TravelAlbum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService imageService;

        public HomeController(IImageService imageService)
        {
            Guard.WhenArgument(imageService, "imageService").IsNull().Throw();
            this.imageService = imageService;
        }

        public ActionResult Index(int pageIndex = 0)
        {
            return View();
        }

        public JsonResult GetImagesOnScroll(string url, int pageIndex = 0)
        {
            var orderedImages = this.imageService.GetLatesImages(pageIndex);

            List<ImagePreviewOutputViewModel> images = new List<ImagePreviewOutputViewModel>();
            if (orderedImages != null && orderedImages.Count() > 0)
            {
                GenerateImageModel(orderedImages, images);

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

        public void GenerateImageModel(IEnumerable<Image> orderedImages, List<ImagePreviewOutputViewModel> images)
        {
            foreach (var image in orderedImages)
            {
                ImagePreviewOutputViewModel imagePreviewOutputViewModel = new ImagePreviewOutputViewModel();
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

                string imagePreviewData = Convert.ToBase64String(image.PreviewContent);

                imagePreviewOutputViewModel.ImageId = image.TravelObjectId;
                imagePreviewOutputViewModel.ImageData = imagePreviewData;
                imagePreviewOutputViewModel.CreatedOn = image.CreatedOn;
                images.Add(imagePreviewOutputViewModel);
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