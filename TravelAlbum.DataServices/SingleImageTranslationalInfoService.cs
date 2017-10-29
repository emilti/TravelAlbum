using Bytes2you.Validation;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class SingleImageTranslationalInfoService : ISingleImageTranslationalInfoService
    {
        private readonly IEfDbSetWrapper<SingleImageTranslationalInfo> singleImageTranslationalInfoSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public SingleImageTranslationalInfoService(IEfDbSetWrapper<SingleImageTranslationalInfo> singleImageTranslationalInfoSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(singleImageTranslationalInfoSetWrapper, "singleImageTranslationalInfoSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();
            
            this.singleImageTranslationalInfoSetWrapper = singleImageTranslationalInfoSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public void Add(SingleImageTranslationalInfo singleImageTranslationalInfo)
        {
            singleImageTranslationalInfoSetWrapper.Add(singleImageTranslationalInfo);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();            
        }
    }
}
