using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using System.Linq;
using TravelAlbum.Web.Models.TravelModels;
using System.IO;
using System.Web;
using TravelAlbum.Web.Models;
using System.Collections.Generic;
using TravelAlbum.Web.Helpers;
using TravelAlbum.Web.Utils;

namespace TravelAlbum.Web.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;

        private readonly ITravelTranslationalInfoService travelTranslationalService;

        private readonly IImageService imageService;

        private readonly IUtils utils;

        public TravelsController(ITravelService travelService, ITravelTranslationalInfoService travelTranslationalService, IImageService imageService, IUtils utils)
        {
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();
            Guard.WhenArgument(travelTranslationalService, "travelTranslationalService").IsNull().Throw();
            Guard.WhenArgument(imageService, "imageService").IsNull().Throw();
            Guard.WhenArgument(utils, "utils").IsNull().Throw();

            this.travelService = travelService;
            this.travelTranslationalService = travelTranslationalService;
            this.imageService = imageService;
            this.utils = utils;
        }

        [ValidateAntiForgeryTokenAttribute]
        public ActionResult SearchTravels(TravelSearchInputViewModel inputModel)
        {
            TravelTranslationalInfo travelByTitle = travelService.GetTravelByTitle(inputModel.Search);

            return RedirectToAction("Details", "Travels", new { id = travelByTitle.TravelObjectId });
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            Travel travel = this.travelService.GetById(id);
            if (travel != null)
            {
                return GetModelData(travel);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        private ActionResult GetModelData(Travel travel)
        {
            int language = utils.GetCurrentLanguage(this);
            IEnumerable<TravelTranslationalInfo> travelTranslationalInfoes =
                travel.TranslatedTravels.AsQueryable().Where(x => x.Language == (Language)language).ToList();

            if (travelTranslationalInfoes.ToList().Count > 1)
            {
                return this.RedirectToAction("Index", "Home");
            }

            Image travelImage = travel.Images.FirstOrDefault();

            String imageData = Convert.ToBase64String(travelImage.Content);
            DetailsTravelOutputViewModel travelViewModel = new DetailsTravelOutputViewModel()
            {
                Id = travel.TravelObjectId,
                Title = travelTranslationalInfoes.First().Title,
                Description = travelTranslationalInfoes.First().Description,
                ImageData = imageData
            };

            return this.View(travelViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TravelInputModel travelForAdding)
        {
            Travel newTravel = new Travel
            {
                TravelObjectId = Guid.NewGuid(),
                StartDate = travelForAdding.StartDate,
                EndDate = travelForAdding.EndDate
            };

            travelService.Add(newTravel);

            TravelTranslationalInfo newBgTravelInfo = new TravelTranslationalInfo()
            {
                TravelTranslationalInfoId = Guid.NewGuid(),
                Title = travelForAdding.bgTitle,
                Description = travelForAdding.bgDescription,
                TravelObject = newTravel,
                Language = Language.Bulgarian
            };

            travelTranslationalService.Add(newBgTravelInfo);
            newTravel.TranslatedTravels.Add(newBgTravelInfo);

            TravelTranslationalInfo newEnTravelInfo = new TravelTranslationalInfo()
            {
                TravelTranslationalInfoId = Guid.NewGuid(),
                Title = travelForAdding.enTitle,
                Description = travelForAdding.enDescription,
                TravelObject = newTravel,
                Language = Language.English
            };

            travelTranslationalService.Add(newEnTravelInfo);
            newTravel.TranslatedTravels.Add(newEnTravelInfo);
            return this.RedirectToAction("Details", "Travels", new { id = newTravel.TravelObjectId });
        }

        private void GenerateImage(HttpPostedFileBase image, HttpPostedFileBase previewImage, DateTime createdOn, Travel newTravel)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                imageData = binaryReader.ReadBytes(image.ContentLength);
            }

            byte[] previewImageData = null;

            using (var binaryReader = new BinaryReader(previewImage.InputStream))
            {
                previewImageData = binaryReader.ReadBytes(previewImage.ContentLength);
            }

            Image newTravelImage = new Image
            {
                TravelObjectId = Guid.NewGuid(),
                Content = imageData,
                PreviewContent = previewImageData,
                CreatedOn = createdOn,
                Travel = newTravel,
                TravelId = newTravel.TravelObjectId
            };

            imageService.Add(newTravelImage);
        }

        [HttpGet]
        public ActionResult GetTravels(string url, int pageIndex = 0)
        {
            var orderedTravels = this.travelService.GetLatesTravels(pageIndex);

            TravelsOutputViewModel travelsOutputViewModel = new TravelsOutputViewModel();
            if (orderedTravels != null && orderedTravels.Count() > 0)
            {
                foreach (var travel in orderedTravels)
                {
                    TravelSummaryOutputViewModel travelSummaryOutputViewModel = new TravelSummaryOutputViewModel();
                    
                    TranslatedData translatedData = new TranslatedData();

                    translatedData = GetTranslatedData(travel);

                    string imageData = Convert.ToBase64String(travel.Images.First().Content);

                    travelSummaryOutputViewModel.Id = travel.TravelObjectId;
                    travelSummaryOutputViewModel.Title = translatedData.Title;
                    travelSummaryOutputViewModel.Description = translatedData.Descrption;
                    travelSummaryOutputViewModel.ImageData = imageData;
                    travelsOutputViewModel.travels.Add(travelSummaryOutputViewModel);
                }

                return this.View(travelsOutputViewModel);
            }
            else
            {
                return this.View(travelsOutputViewModel);
            }
        }


        private TranslatedData GetTranslatedData(Travel travel)
        {
            int language = utils.GetCurrentLanguage(this);
            IEnumerable<TravelTranslationalInfo> infos =
                                    travel.TranslatedTravels.Where(x => x.Language == (Language)language).ToList();
            TravelTranslationalInfo travelTranslationalInfo = infos.First();
            TranslatedData translatedData = new TranslatedData(travelTranslationalInfo.Title, travelTranslationalInfo.Description);
            return translatedData;
        }

        [HttpGet]
        public ActionResult All()
        {
            return this.View();
        }
    }
}
