using System;

namespace TravelAlbum.Web.Models.SingleImageModels
{
    public class SingleImageOutputViewModel
    {
        public Guid SingleImageId { get; set; }

        public string SingleImageData { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}