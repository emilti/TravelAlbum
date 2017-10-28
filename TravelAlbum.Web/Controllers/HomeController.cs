using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Models.TravelModels;

namespace TravelAlbum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITravelService travelService;

        public HomeController(ITravelService travelService)
        {
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();          

            this.travelService = travelService;
           
        }
        public ActionResult Index()
        {            
            var orderedTravels = this.travelService.GetLatesTravels();
            LatestTravelsOutputModel latestTravelsOutputModel = new LatestTravelsOutputModel();
            latestTravelsOutputModel.travels = orderedTravels;            
            return View(latestTravelsOutputModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "TEST!2Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}