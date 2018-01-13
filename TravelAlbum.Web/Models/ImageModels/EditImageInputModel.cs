using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TravelAlbum.Models;
using TravelAlbum.Resources.App_GlobalResources;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class EditImageInputModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "BgTitleLabel", ResourceType = typeof(GlobalResources))]
        public string bgTitle { get; set; }

        [Display(Name = "EnTitleLabel", ResourceType = typeof(GlobalResources))]
        public string enTitle { get; set; }


        [Display(Name = "BgDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string bgDescription { get; set; }

        [Display(Name = "EnDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string enDescription { get; set; }

        public IEnumerable<SelectListItem> TravelsDropDown { get; set; }

        [Required]
        [Display(Name = "ImageCreatedOnLabel", ResourceType = typeof(GlobalResources))]
        public DateTime CreatedOn { get; set; }
     
        public Guid TravelObjectId { get; set; }

        public virtual Travel Travel { get; set; }
    }
}