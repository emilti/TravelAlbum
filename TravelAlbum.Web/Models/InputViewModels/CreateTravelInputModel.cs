using System.Web;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.InputViewModels
{
    public class CreateTravelInputModel
    { 
        public string Title { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public Language Language { get; set; }
    }
}