using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.TravelModels
{
    public class DetailsTravelOutputViewModel
    {
        public Guid Id { get; set; }      

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageData { get; set; }

        public string BackgroundData { get; set; }
    }
}