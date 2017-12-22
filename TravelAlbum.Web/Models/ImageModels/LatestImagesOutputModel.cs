using System.Collections.Generic;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class LatestImagesOutputModel
    {
        public LatestImagesOutputModel()
        {
            this.singleImagePreviews = new List<ImagePreviewOutputViewModel>();
        }

        public List<ImagePreviewOutputViewModel> singleImagePreviews { get; set; }
    }
}