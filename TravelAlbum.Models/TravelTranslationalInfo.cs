using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TravelAlbum.Models
{
    public class TravelTranslationalInfo
    {
        [Key]
        public Guid TravelTranslationalInfoId { get; set; }

        public Guid TravelObjectId { get; set; }
        
        [ForeignKey("TravelObjectId")]
        public virtual Travel TravelObject { get; set; }
        
        public Language Language { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Title { get; set; }


        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(20000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Description { get; set; }
    }
}
