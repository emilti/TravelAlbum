using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class ImagePreviewOutputViewModel
    {
        public Guid ImageId { get; set; }

        public string ImageData { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }
    }
}