using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Web.Models.SingleImageModels
{
    public class SingleImageOutputViewModel
    {
        public Guid SingleImageId { get; set; }

        public string SingleImageData { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
    }
}