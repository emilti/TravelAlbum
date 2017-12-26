using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Models.ImageModels;
using TravelAlbum.Web.Models.ImageModels;
using TravelAlbum.Web.Helpers;
using TravelAlbum.Web.Models;

namespace TravelAlbum.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService imageService;

        private readonly IMountainsService mountainsService;

        private readonly IImageTranslationalInfoService imageTranslationalInfoService;


        public ImagesController(IImageService imageService, IMountainsService mountainsService, IImageTranslationalInfoService imageTranslationalInfoService)
        {
            Guard.WhenArgument(imageService, "imageService").IsNull().Throw();
            Guard.WhenArgument(mountainsService, "mountainsService").IsNull().Throw();
            Guard.WhenArgument(imageTranslationalInfoService, "imageTranslationalInfoService").IsNull().Throw();

            this.imageService = imageService;
            this.mountainsService = mountainsService;
            this.imageTranslationalInfoService = imageTranslationalInfoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            var mountains = this.mountainsService.All().ToList();
            ImageInputModel model = new ImageInputModel();
            model.MountainsDropDown = this.GetMountainsSelectList();
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(ImageInputModel imageForAdding)
        {
            if (this.ModelState.IsValid)
            {
                HttpPostedFileBase imageContent = imageForAdding.UploadedImage;
                

                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(imageContent.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageContent.ContentLength);
                }

                HttpPostedFileBase imagePreviewContent = imageForAdding.UploadedPreviewImage;
              

                byte[] previewImageData = null;

                using (var binaryReader = new BinaryReader(imagePreviewContent.InputStream))
                {
                    previewImageData = binaryReader.ReadBytes(imagePreviewContent.ContentLength);
                }


                Mountain mountain = this.mountainsService.GetById(imageForAdding.MountainId);

                Image newImage = new Image
                {
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Content = imageData,
                    PreviewContent = previewImageData,
                    MountainId = mountain.MountainId,
                    Mountain = mountain
                };

                imageService.Add(newImage);

                ImageTranslationalInfo newBgImageTranslationalInfo = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Description = imageForAdding.bgDescription,
                    Image = newImage,
                    TravelObjectId = newImage.TravelObjectId,
                    Language = Language.Bulgarian
                };

                imageTranslationalInfoService.Add(newBgImageTranslationalInfo);
                newImage.TranslatedInfoes.Add(newBgImageTranslationalInfo);

                ImageTranslationalInfo newEnImageTranslationalInfo = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Description = imageForAdding.enDescription,
                    Image = newImage,
                    TravelObjectId = newImage.TravelObjectId,
                    Language = Language.English
                };

                imageTranslationalInfoService.Add(newEnImageTranslationalInfo);
                newImage.TranslatedInfoes.Add(newEnImageTranslationalInfo);
            }

            return this.RedirectToAction("SearchImages", "Images");
        }


        [HttpGet]
        public ActionResult Details(Guid id)
        {
            Image image = this.imageService.GetById(id);
            if (image != null)
            {
                string query = Request.Url.PathAndQuery;

                if (!(query.Contains("/en/")))
                {
                    return GetModelData(image, Language.Bulgarian);
                }
                else
                {
                    return GetModelData(image, Language.English);
                }
            }
            else
            {
                return this.RedirectToAction("SearchImages", "Images");
            }
        }

        private ActionResult GetModelData(Image image, Language language)
        {
            IEnumerable<ImageTranslationalInfo> imageTranslationalInfoes =
                image.TranslatedInfoes.AsQueryable().Where(x => x.Language == language).ToList();

            if (imageTranslationalInfoes.ToList().Count > 1)
            {
                return this.RedirectToAction("SearchImages", "Images");
            }

            ImageTranslationalInfo imageTranslationalInfo =
                imageTranslationalInfoes.FirstOrDefault();

            String imageData = Convert.ToBase64String(image.Content);

            string description = imageTranslationalInfo == null ? "No description" : imageTranslationalInfo.Description;
            ImageOutputViewModel imageOutputViewModel = new ImageOutputViewModel()
            {
                ImageId = image.TravelObjectId,
                CreatedOn = image.CreatedOn,
                ImageData = imageData,
                Description = description
            };

            return this.View(imageOutputViewModel);
        }

        private IEnumerable<SelectListItem> GetMountainsSelectList()
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            string query = Request.Url.PathAndQuery;

            var mountains = this.mountainsService.All().ToList();
            var mountainsTranslations = new List<MountainTranslationalInfo>();
            if (!(query.Contains("/en")))
            {
                 mountainsTranslations = mountains.SelectMany(a => a.TranslatedInfoes).Where(a => a.Language == Language.Bulgarian).ToList();
            }
            else
            {
                mountainsTranslations = mountains.SelectMany(a => a.TranslatedInfoes).Where(a => a.Language == Language.English).ToList();
            }
            
            
            foreach (var mountain in mountains)
            {
                
                selectList.Add(new SelectListItem
                {
                    Value = mountain.MountainId.ToString(),
                    Text = mountainsTranslations.FirstOrDefault(a => a.MountainId == mountain.MountainId).Name.ToString()
                });
            }

            return selectList;
        }

        [HttpGet]
        public ActionResult SearchImages(ImagesListViewModel model)
        {
            model.MountainsDropDown = this.GetMountainsSelectList();
        
            if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            if (model.MountainsIds != null)
            {
                var images = this.imageService.GetImagesByMountain(model.MountainsIds.ToList(), (int)(model.SelectedSorting)).ToList();
                model.TotalPages = images.Count() / 4;
                if(images.Count() % 4 > 0)
                {
                    model.TotalPages = model.TotalPages + 1;
                }

                images = images.Skip((model.CurrentPage - 1) * 4).Take(4).ToList().ToList();
                foreach (var image in images)
                {
                    ImagePreviewOutputViewModel imagePreviewOutputViewModel = new ImagePreviewOutputViewModel();

                    string imagePreviewData = Convert.ToBase64String(image.PreviewContent);

                    imagePreviewOutputViewModel.ImageId = image.TravelObjectId;
                    imagePreviewOutputViewModel.ImageData = imagePreviewData;
                    imagePreviewOutputViewModel.CreatedOn = image.CreatedOn;
                    model.ImagePreviews.Add(imagePreviewOutputViewModel);
                }
            }
            else
            {
                model.MountainsIds = new List<Guid>();
            }            

            return this.View(model);
        }
    }
}