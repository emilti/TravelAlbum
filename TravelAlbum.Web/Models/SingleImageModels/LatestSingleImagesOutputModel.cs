using System.Collections.Generic;

namespace TravelAlbum.Web.Models.SingleImageModels
{
    public class LatestSingleImagesOutputModel
    {
        public LatestSingleImagesOutputModel()
        {
            this.singleImagePreviews = new List<SingleImagePreviewOutputViewModel>();
        }

        public List<SingleImagePreviewOutputViewModel> singleImagePreviews { get; set; }
    }
}