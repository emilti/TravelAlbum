using System;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface IMountainsService
    {
        void Add(Mountain mountain);

        Mountain GetById(Guid? id);

        IEnumerable<Mountain> All();
    }
}
