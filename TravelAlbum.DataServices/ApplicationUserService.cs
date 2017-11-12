using Bytes2you.Validation;
using System;
using System.Linq;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IEfDbSetWrapper<ApplicationUser> applicationUserSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public ApplicationUserService(IEfDbSetWrapper<ApplicationUser> applicationUserSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(applicationUserSetWrapper, "applicationUserSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.applicationUserSetWrapper = applicationUserSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public ApplicationUser GetUserDetails(string id)
        {
            return this.applicationUserSetWrapper.GetById(id);
        }

        public IQueryable<ApplicationUser> All()
        {
            return this.applicationUserSetWrapper.All;
        }
    }
}
