using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using System.Linq;
using TravelAlbum.Web.Models.InputViewModels;

namespace TravelAlbum.Web.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;

        private readonly ITravelTranslationalInfoService travelTranslationalService;



        public TravelsController(ITravelService travelService, ITravelTranslationalInfoService travelTranslationalService)
        {
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();
            Guard.WhenArgument(travelService, "travelTranslationalService").IsNull().Throw();

            this.travelService = travelService;
            this.travelTranslationalService = travelTranslationalService;
        }

        // public ActionResult SearchTravels()
        // {
        //     return this.View();
        // }

        [ValidateAntiForgeryTokenAttribute]
        public ActionResult SearchTravels(TravelSearchInputViewModel inputModel)
        {
            TravelTranslationalInfo travelByTitle = travelService.GetTravelByTitle(inputModel.Search);
            return RedirectToAction("Details", "Travels", new { id = travelByTitle.TravelId });
            // Travel travel = this.travelService.GetById(id);
        }

        [HttpGet]
        public ActionResult Details(Guid? id)
        {
            Travel travel = this.travelService.GetById(id);
            TravelTranslationalInfo travelTranslationalInfo = travel.TranslatedTravels.FirstOrDefault(x => x.TravelId == travel.Id);
            TravelViewModel travelViewModel = new TravelViewModel()
            {
                Title = travelTranslationalInfo.Title,
                Description = travelTranslationalInfo.Description,
                Language = travelTranslationalInfo.Language
            };
            
            // BookViewModel viewModel = new BookViewModel(book);

            return this.View(travelViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Add(CreateTravelInputModel travelForAdding)
        {
            Travel newTravel = new Travel
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2017, 08, 10),
                EndDate = new DateTime(2017, 08, 11)             
            };


            travelService.Add(newTravel);

            TravelTranslationalInfo newTravelTranslationalInfo = new TravelTranslationalInfo
            {
                Id = Guid.NewGuid(),
                Title = travelForAdding.Title,
                Description = travelForAdding.Description,
                Language = Languages.English,
                Travel = newTravel
            };

            
            travelTranslationalService.Add(newTravelTranslationalInfo);

            return this.RedirectToAction("Details", "Travels", new { id = newTravel.Id });
        }

        [HttpGet]
        public ActionResult All()
        {
            return this.View();
        }
    }
}