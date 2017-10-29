using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.SingleImageModels
{
    public class LatestSingleImagesOutputModel
    {
        public LatestSingleImagesOutputModel()
        {
            this.singleImages = new List<SingleImageOutputViewModel>();
        }

        public List<SingleImageOutputViewModel> singleImages { get; set; }
    }
}