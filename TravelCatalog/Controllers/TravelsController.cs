using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using TravelCatalog.DataServices.Contracts;
using TravelCatalog.Models;

namespace TravelCatalog.Controllers
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
            Travel travelByTitle = travelService.GetTravelByTitle(inputModel.Search);
            return RedirectToAction("Details", "Travels", new { id = travelByTitle.Id });
            // Travel travel = this.travelService.GetById(id);
        }

        [HttpGet]
        public ActionResult Details(Guid? id)
        {
            Travel travel = this.travelService.GetById(id);

            // BookViewModel viewModel = new BookViewModel(book);

            return this.View(travel);
        }

        [HttpGet]
        public ActionResult All()
        {
            return this.View();
        }
    }
}