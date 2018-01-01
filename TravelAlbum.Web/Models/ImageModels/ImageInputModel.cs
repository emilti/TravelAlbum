﻿using System;
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
        [Display(Name = "UploadedImage1Label", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedImage { get; set; }

        [Required]
        [Display(Name = "UploadedImage1PreviewLabel", ResourceType = typeof(GlobalResources))]
        public HttpPostedFileBase UploadedPreviewImage { get; set; }

        public IEnumerable<Guid> MountainsIds { get; set; }

        public IEnumerable<SelectListItem> MountainsDropDown { get; set; }

        public IEnumerable<SelectListItem> TravelsDropDown { get; set; }

        [Required]
        [Display(Name = "ImageCreatedOnLabel", ResourceType = typeof(GlobalResources))]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "MountainDropdownLabel", ResourceType = typeof(GlobalResources))]
        public Guid MountainId { get; set; }

        public virtual Mountain Mountain { get; set; }
          
        public Guid TravelObjectId { get; set; }

        public virtual Travel Travel { get; set; }
    }
}