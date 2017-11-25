using Bytes2you.Validation;
using System;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class TravelObjectService : ITravelObjectService
    {
        private readonly IEfDbSetWrapper<TravelObject> travelObjectSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public TravelObjectService(IEfDbSetWrapper<TravelObject> travelObjectSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(travelObjectSetWrapper, "travelObjectSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.travelObjectSetWrapper = travelObjectSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public TravelObject GetById(Guid? id)
        {
            TravelObject result = null;

            TravelObject travel = this.travelObjectSetWrapper.GetById(id);
            if (travel != null)
            {
                result = travel;
            }

            return result;
        }

    }
}
