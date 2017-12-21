using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.Models;
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