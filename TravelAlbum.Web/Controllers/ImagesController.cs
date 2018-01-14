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
using TravelAlbum.Web.Utils;
using TravelAlbum.Web.Helpers;

namespace TravelAlbum.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService imageService;

        private readonly IMountainsService mountainsService;

        private readonly IImageTranslationalInfoService imageTranslationalInfoService;

        private readonly ITravelService travelService;

        private readonly IUtils utils;

        public ImagesController(IImageService imageService, IMountainsService mountainsService, IImageTranslationalInfoService imageTranslationalInfoService, ITravelService travelService, IUtils utils)
        {
            Guard.WhenArgument(imageService, "imageService").IsNull().Throw();
            Guard.WhenArgument(mountainsService, "mountainsService").IsNull().Throw();
            Guard.WhenArgument(imageTranslationalInfoService, "imageTranslationalInfoService").IsNull().Throw();
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();
            Guard.WhenArgument(utils, "utils").IsNull().Throw();
            this.imageService = imageService;
            this.mountainsService = mountainsService;
            this.imageTranslationalInfoService = imageTranslationalInfoService;
            this.travelService = travelService;
            this.utils = utils;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            var mountains = this.mountainsService.All().ToList();
            AddImageInputModel model = new AddImageInputModel();
            model.MountainsDropDown = this.GetMountainsSelectList();
            model.TravelsDropDown = this.GetTravelsSelectList();
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(AddImageInputModel imageForAdding)
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

                Travel travel = this.travelService.GetById(imageForAdding.TravelObjectId);
                Image newImage = new Image
                {
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = imageForAdding.CreatedOn,
                    Content = imageData,
                    PreviewContent = previewImageData,
                    MountainId = mountain.MountainId,
                    Mountain = mountain,
                    TravelId = imageForAdding.TravelObjectId,
                    Travel = travel
                };

                imageService.Add(newImage);
                travel.Images.Add(newImage);

                ImageTranslationalInfo newBgImageTranslationalInfo = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Title = imageForAdding.bgTitle,
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
                    Title = imageForAdding.enTitle,
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
                return GetModelData(image);
            }
            else
            {
                return this.RedirectToAction("SearchImages", "Images");
            }
        }

        private ActionResult GetModelData(Image image)
        {
            int language = this.utils.GetCurrentLanguage(this);
            IEnumerable<ImageTranslationalInfo> imageTranslationalInfoes =
                image.TranslatedInfoes.AsQueryable().Where(x => x.Language == (Language)language).ToList();

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

            byte[] backgroundImageContent = this.utils.GetFile("/Content/DBImages/SL373229_preview.JPG");
            string backgroundData = Convert.ToBase64String(backgroundImageContent);
            imageOutputViewModel.BackgroundData = backgroundData;
            return this.View(imageOutputViewModel);
        }

        private IEnumerable<SelectListItem> GetTravelsSelectList()
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            int language = this.utils.GetCurrentLanguage(this);

            var travels = this.travelService.All().ToList();
            var travelTranslations = new List<TravelTranslationalInfo>();

            travelTranslations = travels.SelectMany(a => a.TranslatedTravels).Where(a => a.Language == (Language)language).ToList();

            foreach (var travel in travels)
            {
                selectList.Add(new SelectListItem
                {
                    Value = travel.TravelObjectId.ToString(),
                    Text = travelTranslations.FirstOrDefault(a => a.TravelObjectId == travel.TravelObjectId).Title.ToString()
                });
            }

            selectList.Add(new SelectListItem() { Value = null, Text = "Not assigned to travel" });

            return selectList;
        }

        private IEnumerable<SelectListItem> GetMountainsSelectList()
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            int language = this.utils.GetCurrentLanguage(this);

            var mountains = this.mountainsService.All().ToList();
            var mountainsTranslations = new List<MountainTranslationalInfo>();

            mountainsTranslations = mountains.SelectMany(a => a.TranslatedInfoes).Where(a => a.Language == (Language)language).ToList();

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
            model.PageSizes = this.GetPageSizes(model);

            if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            if (model.MountainsIds != null)
            {
                var images = this.imageService.GetImagesByMountain(model.MountainsIds.ToList(), (int)(model.SelectedSorting), model.SearchedTitle).ToList();
                model.TotalPages = images.Count() / model.SelectedPageSize;
                if (images.Count() % model.SelectedPageSize > 0)
                {
                    model.TotalPages = model.TotalPages + 1;
                }


                // if (model.SearchedTitle != null && model.SearchedTitle != String.Empty)
                // {
                //     var filteredImagesByTitle = images.Where(a => a.TranslatedInfoes.Where(b => b.Title.Contains(model.SearchedTitle)).ToList().Count > 0).ToList();
                // }

                images = images.Skip((model.CurrentPage - 1) * model.SelectedPageSize).Take(model.SelectedPageSize).ToList().ToList();
                foreach (var image in images)
                {
                    ImagePreviewOutputViewModel imagePreviewOutputViewModel = new ImagePreviewOutputViewModel();
                    string query = Request.Url.PathAndQuery;

                    string title = string.Empty;
                    title = this.ValidateTranslatedInfo(image, query, title);

                    string imagePreviewData = Convert.ToBase64String(image.PreviewContent);

                    imagePreviewOutputViewModel.ImageId = image.TravelObjectId;
                    imagePreviewOutputViewModel.ImageData = imagePreviewData;
                    imagePreviewOutputViewModel.CreatedOn = image.CreatedOn;
                    imagePreviewOutputViewModel.Title = title;
                    model.ImagePreviews.Add(imagePreviewOutputViewModel);
                }
            }
            else
            {
                model.MountainsIds = new List<Guid>();
            }

            byte[] backgroundImageContent = this.utils.GetFile("/Content/DBImages/SL373580_preview.JPG");
            string backgroundData = Convert.ToBase64String(backgroundImageContent);
            model.BackgroundData = backgroundData;
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Image imageForEdit = imageService.GetById(id);

            var imageInfoes = imageForEdit.TranslatedInfoes.ToList();
            ImageTranslationalInfo bgImageInfo = imageInfoes.FirstOrDefault(a => a.Language == Language.Bulgarian);
            ImageTranslationalInfo enImageInfo = imageInfoes.FirstOrDefault(a => a.Language == Language.English);

            string bgTitle = bgImageInfo != null ? bgImageInfo.Title : null;
            string enTitle = enImageInfo != null ? enImageInfo.Title : null;

            string bgDescription = bgImageInfo != null ? bgImageInfo.Description : null;
            string enDescription = enImageInfo != null ? enImageInfo.Description : null;

            EditImageInputModel editImageInputModel = new EditImageInputModel()
            {
                Id = id,
                bgTitle = bgTitle,
                bgDescription = bgDescription,
                enTitle = enTitle,
                enDescription = enDescription,
                CreatedOn = imageForEdit.CreatedOn,
                TravelsDropDown = this.GetTravelsSelectList()
            };

            return this.View(editImageInputModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditImageInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                Image imageForEdit = imageService.GetById(model.Id);
                imageForEdit.CreatedOn = model.CreatedOn;
                ImageTranslationalInfo bgImageInfo = imageForEdit.TranslatedInfoes.FirstOrDefault(a => a.Language == Language.Bulgarian);
                if (bgImageInfo == null)
                {
                    bgImageInfo = new ImageTranslationalInfo()
                    {
                        ImageTranslationalInfoId = Guid.NewGuid(),
                        Image = imageForEdit,
                        TravelObjectId = imageForEdit.TravelObjectId,
                        Language = Language.Bulgarian
                    };

                    this.imageTranslationalInfoService.Add(bgImageInfo);
                    imageForEdit.TranslatedInfoes.Add(bgImageInfo);
                }

                bgImageInfo.Title = model.bgTitle;
                bgImageInfo.Description = model.bgDescription;

                ImageTranslationalInfo enImageInfo = imageForEdit.TranslatedInfoes.FirstOrDefault(a => a.Language == Language.English);
                if (enImageInfo == null)
                {
                    enImageInfo = new ImageTranslationalInfo()
                    {
                        ImageTranslationalInfoId = Guid.NewGuid(),
                        Image = imageForEdit,
                        TravelObjectId = imageForEdit.TravelObjectId,
                        Language = Language.English
                    };

                    this.imageTranslationalInfoService.Add(enImageInfo);
                    imageForEdit.TranslatedInfoes.Add(enImageInfo);
                }

                enImageInfo.Title = model.enTitle;
                enImageInfo.Description = model.enDescription;

                Travel travel = this.travelService.GetById(model.TravelObjectId);
                imageForEdit.Travel = travel;
                imageForEdit.TravelId = model.TravelObjectId;

                this.imageService.SaveChanges();
            }

            return this.RedirectToAction("Details", "Images", new { id = model.Id });
        }

        private string ValidateTranslatedInfo(Image image, string query, string title)
        {
            if (image.TranslatedInfoes != null)
            {
                int language = this.utils.GetCurrentLanguage(this);
                var translatedInfo = image.TranslatedInfoes.FirstOrDefault(a => a.Language == (Language)language);
                title = PopulateTitle(title, translatedInfo);
            }

            return title;
        }

        private static string PopulateTitle(string title, ImageTranslationalInfo translatedInfo)
        {
            if (translatedInfo != null)
            {
                title = translatedInfo.Title;
            }

            return title;
        }

        private IEnumerable<SelectListItem> GetPageSizes(ImagesListViewModel model)
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem
            {
                Value = "2",
                Text = "2"
            });

            selectList.Add(new SelectListItem
            {
                Value = "4",
                Text = "4"
            });

            selectList.Add(new SelectListItem
            {
                Value = "10",
                Text = "10"
            });

            return selectList;
        }
    }
}