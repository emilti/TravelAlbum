using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


        [HttpGet]
        public ActionResult Details(Guid id)
        {
            SingleImage singleImage = this.singleImageService.GetById(id);
            if (singleImage != null)
            {
                string query = Request.Url.PathAndQuery;

                if (!(query.Contains("/en/")))
                {
                    return GetModelData(singleImage, Language.Bulgarian);
                }
                else
                {
                    return GetModelData(singleImage, Language.English);
                }
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        private ActionResult GetModelData(SingleImage singleImage, Language language)
        {
            IEnumerable<SingleImageTranslationalInfo> singleImageTranslationalInfoes =
                singleImage.TranslatedInfoes.AsQueryable().Where(x => x.Language == language).ToList();

            if (singleImageTranslationalInfoes.ToList().Count > 1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            SingleImageTranslationalInfo singleImageTranslationalInfo =
                singleImageTranslationalInfoes.FirstOrDefault();

            String imageData = Convert.ToBase64String(singleImage.Content);
            SingleImageOutputViewModel singleImageOutputViewModel = new SingleImageOutputViewModel()
            {
                CreatedOn = singleImage.CreatedOn,
                SingleImageData = imageData,
                Description = singleImageTranslationalInfo.Description
            };

            return this.View(singleImageOutputViewModel);
        }
    }
}