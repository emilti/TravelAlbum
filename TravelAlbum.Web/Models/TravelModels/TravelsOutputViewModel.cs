using System.Collections.Generic;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.TravelModels
{
    public class TravelsOutputViewModel
    {
        public TravelsOutputViewModel()
        {
            this.travels = new List<TravelSummaryOutputViewModel>();            
        }

        public List<TravelSummaryOutputViewModel> travels { get; set; }

        public string BackgroundData { get; set; }
    }
}