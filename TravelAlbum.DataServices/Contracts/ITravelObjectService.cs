using System;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface ITravelObjectService
    {
        TravelObject GetById(Guid? id);
    }
}
