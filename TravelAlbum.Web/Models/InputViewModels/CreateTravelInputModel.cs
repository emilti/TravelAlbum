using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using TravelAlbum.Models;
using TravelAlbum.Resources.App_GlobalResources;

namespace TravelAlbum.Web.Models.InputViewModels
{
    public class CreateTravelInputModel
    {
        [Required]
        [Display(Name = "BgTitleLabel", ResourceType = typeof(GlobalResources))]        
        public string bgTitle { get; set; }

        [Required]
        [Display(Name = "BgDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string bgDescription { get; set; }

        [Required]
        [Display(Name = "EnTitleLabel", ResourceType = typeof(GlobalResources))]
        public string enTitle { get; set; }

        [Required]
        [Display(Name = "EnDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string enDescription { get; set; }

        [Required]
        [Display(Name = "UploadedImage_1", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_1 { get; set; }

        [Required]
        [Display(Name = "UploadedImage_2", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_2 { get; set; }

        [Required]
        [Display(Name = "UploadedImage_3", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_3 { get; set; }

        [Required]
        [Display(Name = "UploadedImage_4", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage_4 { get; set; }

        public Language Language { get; set; }
    }
}