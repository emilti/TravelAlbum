using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAlbum.Resources.App_GlobalResources;

namespace TravelAlbum.Enums
{
    public enum Mountains
    {
        [Display(Name = "RilaMountainLabel", ResourceType = typeof(GlobalResources))]
        Rila    = 0,
        Vitosha = 1,
        Pirin   = 2
    }
}
