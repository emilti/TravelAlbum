using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAlbum.Resources.App_GlobalResources;

namespace TravelAlbum.Web.Enums
{
    public enum Sorting
    {
        [Display(Name = "SortingAscendingLabel", ResourceType = typeof(GlobalResources))]
        Ascending = 1,
        [Display(Name = "SortingDescendingLabel", ResourceType = typeof(GlobalResources))]
        Descending = 2
    }
}
