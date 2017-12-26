using Bytes2you.Validation;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class ImageTranslationalInfoService : IImageTranslationalInfoService
    {
        private readonly IEfDbSetWrapper<ImageTranslationalInfo> imageTranslationalInfoSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public ImageTranslationalInfoService(IEfDbSetWrapper<ImageTranslationalInfo> imageTranslationalInfoSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(imageTranslationalInfoSetWrapper, "imageTranslationalInfoSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();
            
            this.imageTranslationalInfoSetWrapper = imageTranslationalInfoSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public void Add(ImageTranslationalInfo imageTranslationalInfo)
        {
            imageTranslationalInfoSetWrapper.Add(imageTranslationalInfo);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();            
        }
    }
}
