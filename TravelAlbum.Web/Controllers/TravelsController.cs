using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using System.Linq;

namespace TravelAlbum.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;
        

        public TravelsController(ITravelService travelService)
        {
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();    

            this.travelService = travelService;           
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
        public ActionResult All()
        {
            return this.View();
        }
    }
}