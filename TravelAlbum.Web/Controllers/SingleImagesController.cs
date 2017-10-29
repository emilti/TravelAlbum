using Bytes2you.Validation;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Models.SingleImageModels;

namespace TravelAlbum.Web.Controllers
{
    public class SingleImagesController : Controller
    {
        private readonly ISingleImageService singleImageService;

        private readonly ISingleImageTranslationalInfoService singleImageTranslationalInfoService;


        public SingleImagesController(ISingleImageService singleImageService, ISingleImageTranslationalInfoService singleImageTranslationalInfoService)
        {
            Guard.WhenArgument(singleImageService, "singleImageService").IsNull().Throw();
            Guard.WhenArgument(singleImageTranslationalInfoService, "singleImageTranslationalInfoService").IsNull().Throw();

            this.singleImageService = singleImageService;
            this.singleImageTranslationalInfoService = singleImageTranslationalInfoService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Add(SingleImageInputModel singleImageForAdding)
        {
            HttpPostedFileBase singleImageContent = singleImageForAdding.UploadedImage;
            var imageContent = new byte[singleImageContent.ContentLength];

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(singleImageContent.InputStream))
            {
                imageData = binaryReader.ReadBytes(singleImageContent.ContentLength);
            }

            SingleImage newSingleImage = new SingleImage
            {
                SingleImageId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Content = imageData
            };

            singleImageService.Add(newSingleImage);

            SingleImageTranslationalInfo newBgSingleImageTranslationalInfo = new SingleImageTranslationalInfo()
            {
                SingleImageTranslationalInfoId = Guid.NewGuid(),
                Description = singleImageForAdding.bgDescription,
                SingleImage = newSingleImage,
                SingleImageId = newSingleImage.SingleImageId,
                Language = Language.Bulgarian
            };

            singleImageTranslationalInfoService.Add(newBgSingleImageTranslationalInfo);
            newSingleImage.TranslatedInfoes.Add(newBgSingleImageTranslationalInfo);

            SingleImageTranslationalInfo newEnSingleImageTranslationalInfo = new SingleImageTranslationalInfo()
            {
                SingleImageTranslationalInfoId = Guid.NewGuid(),
                Description = singleImageForAdding.enDescription,
                SingleImage = newSingleImage,
                SingleImageId = newSingleImage.SingleImageId,
                Language = Language.English
            };

            singleImageTranslationalInfoService.Add(newEnSingleImageTranslationalInfo);
            newSingleImage.TranslatedInfoes.Add(newEnSingleImageTranslationalInfo);

            return this.RedirectToAction("Index", "Home");
        }
    }
}