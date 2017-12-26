using System.Collections.Generic;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class LatestImagesOutputModel
    {
        public LatestImagesOutputModel()
        {
            this.ImagePreviews = new List<ImagePreviewOutputViewModel>();
        }

        public List<ImagePreviewOutputViewModel> ImagePreviews { get; set; }
    }
}