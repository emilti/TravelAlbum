using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAlbum.Models
{
    public class TravelViewModel
    {
        public Guid Id { get; set; }      

        public Language Language { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageData { get; set; }
    }
}