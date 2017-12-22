using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class ImagePreviewOutputViewModel
    {
        public Guid SingleImageId { get; set; }

        public string SingleImageData { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
    }
}