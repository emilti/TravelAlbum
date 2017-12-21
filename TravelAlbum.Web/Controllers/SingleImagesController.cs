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

        private readonly IMountainsService mountainsService;

        private readonly ISingleImageTranslationalInfoService singleImageTranslationalInfoService;


        public SingleImagesController(ISingleImageService singleImageService, IMountainsService mountainsService, ISingleImageTranslationalInfoService singleImageTranslationalInfoService)
        {
            Guard.WhenArgument(singleImageService, "singleImageService").IsNull().Throw();
            Guard.WhenArgument(mountainsService, "mountainsService").IsNull().Throw();
            Guard.WhenArgument(singleImageTranslationalInfoService, "singleImageTranslationalInfoService").IsNull().Throw();

            this.singleImageService = singleImageService;
            this.mountainsService = mountainsService;
            this.singleImageTranslationalInfoService = singleImageTranslationalInfoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            var mountains = this.mountainsService.All().ToList();
            SingleImageInputModel model = new SingleImageInputModel();
            model.MountainsDropDown = this.GetMountainsSelectList(mountains);
            return this.View(model);           
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(SingleImageInputModel singleImageForAdding)
        {
            if (this.ModelState.IsValid)
            {
                HttpPostedFileBase singleImageContent = singleImageForAdding.UploadedImage;
                var imageContent = new byte[singleImageContent.ContentLength];

                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(singleImageContent.InputStream))
                {
                    imageData = binaryReader.ReadBytes(singleImageContent.ContentLength);
                }

                HttpPostedFileBase singleImagePreviewContent = singleImageForAdding.UploadedPreviewImage;
                var previewImageContent = new byte[singleImagePreviewContent.ContentLength];

                byte[] previewImageData = null;

                using (var binaryReader = new BinaryReader(singleImagePreviewContent.InputStream))
                {
                    previewImageData = binaryReader.ReadBytes(singleImagePreviewContent.ContentLength);
                }


                Mountain mountain = this.mountainsService.GetById(singleImageForAdding.MountainId);

                SingleImage newSingleImage = new SingleImage
                {
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Content = imageData,
                    PreviewContent = previewImageData,
                    MountainId = mountain.MountainId,
                    Mountain = mountain
                };

                singleImageService.Add(newSingleImage);

                SingleImageTranslationalInfo newBgSingleImageTranslationalInfo = new SingleImageTranslationalInfo()
                {
                    SingleImageTranslationalInfoId = Guid.NewGuid(),
                    Description = singleImageForAdding.bgDescription,
                    SingleImage = newSingleImage,
                    TravelObjectId = newSingleImage.TravelObjectId,
                    Language = Language.Bulgarian
                };

                singleImageTranslationalInfoService.Add(newBgSingleImageTranslationalInfo);
                newSingleImage.TranslatedInfoes.Add(newBgSingleImageTranslationalInfo);

                SingleImageTranslationalInfo newEnSingleImageTranslationalInfo = new SingleImageTranslationalInfo()
                {
                    SingleImageTranslationalInfoId = Guid.NewGuid(),
                    Description = singleImageForAdding.enDescription,
                    SingleImage = newSingleImage,
                    TravelObjectId = newSingleImage.TravelObjectId,
                    Language = Language.English
                };

                singleImageTranslationalInfoService.Add(newEnSingleImageTranslationalInfo);
                newSingleImage.TranslatedInfoes.Add(newEnSingleImageTranslationalInfo);
            }

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

            string description = singleImageTranslationalInfo == null ? "No description" : singleImageTranslationalInfo.Description;
            SingleImageOutputViewModel singleImageOutputViewModel = new SingleImageOutputViewModel()
            {
                SingleImageId = singleImage.TravelObjectId,
                CreatedOn = singleImage.CreatedOn,
                SingleImageData = imageData,
                Description = description
            };

            return this.View(singleImageOutputViewModel);
        }

        private IEnumerable<SelectListItem> GetMountainsSelectList(IEnumerable<Mountain> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.MountainId.ToString(),
                    Text = element.Name
                });
            }

            return selectList;
        }
    }
}