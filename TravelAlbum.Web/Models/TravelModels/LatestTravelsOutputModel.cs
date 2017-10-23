using System.Collections.Generic;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.TravelModels
{
    public class LatestTravelsOutputModel
    {
        public IEnumerable<Travel> travels { get; set; }
    }
}