using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using TravelAlbum.Models;
using TravelAlbum.Web.App_GlobalResources;

namespace TravelAlbum.Web.Models.InputViewModels
{
    public class CreateTravelInputModel
    {
        [Display(Name = "BgTitleLabel", ResourceType = typeof(GlobalResources))]
        public string bgTitle { get; set; }

        [Display(Name = "BgDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string bgDescription { get; set; }
        
        [Display(Name = "EnTitleLabel", ResourceType = typeof(GlobalResources))]
        public string enTitle { get; set; }

        [Display(Name = "EnDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string enDescription { get; set; }

        [Display(Name = "UploadedImage_1", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_1 { get; set; }

        [Display(Name = "UploadedImage_2", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_2 { get; set; }

        [Display(Name = "UploadedImage_3", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_3 { get; set; }

        [Display(Name = "UploadedImage_4", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_4 { get; set; }

        public Language Language { get; set; }
    }
}