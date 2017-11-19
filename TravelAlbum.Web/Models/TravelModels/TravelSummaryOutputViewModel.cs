using System;

namespace TravelAlbum.Web.Models.TravelModels
{
    public class TravelSummaryOutputViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } 
        
        public string Description { get; set; }

        public string ImageData { get; set; }
    }
}