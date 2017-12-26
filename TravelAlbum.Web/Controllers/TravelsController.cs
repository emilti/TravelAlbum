using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using System.Linq;
using TravelAlbum.Web.Models.InputViewModels;
using System.IO;
using TravelAlbum.Web.Models.TravelModels;
using System.Web;
using TravelAlbum.Web.Models;
using System.Collections.Generic;
using TravelAlbum.Web.Helpers;

namespace TravelAlbum.Web.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;

        private readonly ITravelTranslationalInfoService travelTranslationalService;

        private readonly IImageService imageService;

        public TravelsController(ITravelService travelService, ITravelTranslationalInfoService travelTranslationalService, IImageService imageService)
        {
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();
            Guard.WhenArgument(travelTranslationalService, "travelTranslationalService").IsNull().Throw();
            Guard.WhenArgument(imageService, "imageService").IsNull().Throw();

            this.travelService = travelService;
            this.travelTranslationalService = travelTranslationalService;
            this.imageService = imageService;
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
                string query = Request.Url.PathAndQuery;

                if (!(query.Contains("/en/")))
                {
                    return GetModelData(travel, Language.Bulgarian);
                }
                else
                {
                    return GetModelData(travel, Language.English);
                }
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        private ActionResult GetModelData(Travel travel, Language language)
        {
            IEnumerable<TravelTranslationalInfo> travelTranslationalInfoes =
                travel.TranslatedTravels.AsQueryable().Where(x => x.Language == language).ToList();

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
        public ActionResult Add(CreateTravelInputModel travelForAdding)
        {
            Travel newTravel = new Travel
            {
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                StartDate = new DateTime(2017, 08, 10),
                EndDate = DateTime.Now
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

            HttpPostedFileBase image_1 = travelForAdding.UploadedImage_1;
            GenerateImage(image_1, newTravel);

            HttpPostedFileBase image_2 = travelForAdding.UploadedImage_2;
            GenerateImage(image_2, newTravel);

            HttpPostedFileBase image_3 = travelForAdding.UploadedImage_3;
            GenerateImage(image_3, newTravel);

            HttpPostedFileBase image_4 = travelForAdding.UploadedImage_4;
            GenerateImage(image_4, newTravel);

            
            return this.RedirectToAction("Details", "Travels", new { id = newTravel.TravelObjectId });
        }

        private void GenerateImage(HttpPostedFileBase image, Travel newTravel)
        {
            var imageContent = new byte[image.ContentLength];

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                imageData = binaryReader.ReadBytes(image.ContentLength);
            }

            Image newTravelImage = new Image
            {
                TravelObjectId = Guid.NewGuid(),
                Content = imageData,
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
                    // string query = Request.Url.PathAndQuery;

                    TranslatedData translatedData = new TranslatedData();
                    //TODO: fix en and bg string conditions
                    if (!(url.Contains("/en")))
                    {
                        translatedData = GetTranslatedTitle(travel, Language.Bulgarian);
                    }
                    else
                    {
                        translatedData = GetTranslatedTitle(travel, Language.English);
                    }

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


        private TranslatedData GetTranslatedTitle(Travel travel, Language language)
        {
            IEnumerable<TravelTranslationalInfo> infos =
                                    travel.TranslatedTravels.Where(x => x.Language == language).ToList();
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
