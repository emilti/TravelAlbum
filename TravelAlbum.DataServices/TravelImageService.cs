using Bytes2you.Validation;
using TravelAlbum.Data.Contracts;
using TravelAlbum.Data.EfDbSetWrappers;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class TravelImageService : ITravelImageService
    {
       private readonly IEfDbSetWrapper<TravelImage> travelImageSetWrapper;

       private readonly IUnitOfWork unitOfWork;
       
       public TravelImageService(IEfDbSetWrapper<TravelImage> travelImageSetWrapper, IUnitOfWork unitOfWork)
       {
          //  Guard.WhenArgument(travelImageSetWrapper, "travelImageSetWrapper").IsNull().Throw();
          //  Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
          // 
           this.travelImageSetWrapper = travelImageSetWrapper;
           this.unitOfWork = unitOfWork;
       }
       
       public void Add(TravelImage travelImage)
       {
           this.travelImageSetWrapper.Add(travelImage);
           this.unitOfWork.SaveChanges();
       }
    }
}
