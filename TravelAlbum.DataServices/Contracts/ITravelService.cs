using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{

    public interface ITravelService
    {
        Travel GetById(Guid? id);

        TravelTranslationalInfo GetTravelByTitle(string searchTerm);

        void Add(Travel travel);

        IEnumerable<Travel> GetLatesTravels(int pageIndex);
    }
}
