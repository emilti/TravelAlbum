using System;
using System.Linq;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface IMountainsService
    {
        void Add(Mountain mountain);

        Mountain GetById(Guid? id);

        IQueryable<Mountain> All();
    }
}
