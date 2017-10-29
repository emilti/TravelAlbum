using System.ComponentModel.DataAnnotations;
using System.Web;
using TravelAlbum.Web.App_GlobalResources;

namespace TravelAlbum.Web.Models.SingleImageModels
{
    public class SingleImageInputModel
    {

        [Required]
        [Display(Name = "BgDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string bgDescription { get; set; }   

        [Required]
        [Display(Name = "EnDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string enDescription { get; set; }

        [Required]
       //[Display(Name = "UploadedImage_1", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}