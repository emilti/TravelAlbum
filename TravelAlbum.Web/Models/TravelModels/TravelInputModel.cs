using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.Resources.App_GlobalResources;

namespace TravelAlbum.Web.Models.TravelModels
{
    public class TravelInputModel
    {
        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "BgTitleLabel", ResourceType = typeof(GlobalResources))]
        public string bgTitle { get; set; }

        [Required]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(20000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "BgDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string bgDescription { get; set; }

        [Required]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "EnTitleLabel", ResourceType = typeof(GlobalResources))]
        public string enTitle { get; set; }

        [Required]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(20000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "EnDescriptionLabel", ResourceType = typeof(GlobalResources))]
        public string enDescription { get; set; }

        [Required]
        [Display(Name = "TravelStartDateLabel", ResourceType = typeof(GlobalResources))]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "TravelEndDateLabel", ResourceType = typeof(GlobalResources))]
        public DateTime EndDate { get; set; }
    }
}