using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelCatalog.Models
{
    public class TravelViewModel
    {
        public Guid Id { get; set; }      

        public Languages Language { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}