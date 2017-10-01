using System.Web;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.InputViewModels
{
    public class CreateTravelInputModel
    {
        public Languages Language { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        private HttpPostedFileBase uploadImage { get; set; }    
    }
}