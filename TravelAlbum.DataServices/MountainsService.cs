using Bytes2you.Validation;
using System;
using System.Linq;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class MountainsService : IMountainsService
    {
        private readonly IEfDbSetWrapper<Mountain> mountainsSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public MountainsService(IEfDbSetWrapper<Mountain> mountainsSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(mountainsSetWrapper, "mountainsSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.mountainsSetWrapper = mountainsSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public void Add(Mountain mountain)
        {
            mountainsSetWrapper.Add(mountain);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }     

        public Mountain GetById(Guid? id)
        {
            Mountain result = null;

            Mountain mountain = this.mountainsSetWrapper.GetById(id);
            if (mountain != null)
            {
                result = mountain;
            }

            return result;
        }

        public IQueryable<Mountain> All()
        {
            return this.mountainsSetWrapper.All;
        }
    }
}
