using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface IApplicationUserService
    {
        IQueryable<ApplicationUser> All();

        ApplicationUser GetUserDetails(string id);
    }
}
