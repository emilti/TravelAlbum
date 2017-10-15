using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class TravelImageService : ITravelImageService
    {
       private readonly IEfDbSetWrapper<TravelImage> travelImageSetWrapper;

       private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;
       
       public TravelImageService(IEfDbSetWrapper<TravelImage> travelImageSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
       {
          //  Guard.WhenArgument(travelImageSetWrapper, "travelImageSetWrapper").IsNull().Throw();
          //  Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
          // 
           this.travelImageSetWrapper = travelImageSetWrapper;
           this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
       }
       
       public void Add(TravelImage travelImage)
       {
           this.travelImageSetWrapper.Add(travelImage);
           this.travelAlbumEfDbContextSaveChanges.SaveChanges();
       }
    }
}
