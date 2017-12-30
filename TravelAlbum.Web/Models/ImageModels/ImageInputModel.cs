using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.Models;
using TravelAlbum.Resources.App_GlobalResources;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class ImageInputModel
    {
        [Required]        
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "BgTitleLabel", ResourceType = typeof(GlobalResources))]       
        public string bgTitle { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "EnTitleLabel", ResourceType = typeof(GlobalResources))]
        public string enTitle { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "BgDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string bgDescription { get; set; }   

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "EnDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string enDescription { get; set; }

        [Required]
       //[Display(Name = "UploadedImage_1", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage { get; set; }

        [Required]
        //[Display(Name = "UploadedImage_1", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedPreviewImage { get; set; }

        public IEnumerable<Guid> MountainsIds { get; set; }

        public IEnumerable<SelectListItem> MountainsDropDown { get; set; }

        [DisplayName("Планина")]
        public Guid MountainId { get; set; }

        [DisplayName("Планина")]
        public virtual Mountain Mountain { get; set; }        
    }
}