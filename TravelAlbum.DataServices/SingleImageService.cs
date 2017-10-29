using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class SingleImageService : ISingleImageService
    {

        private readonly IEfDbSetWrapper<SingleImage> singleImageSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public SingleImageService(IEfDbSetWrapper<SingleImage> singleImageSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(singleImageSetWrapper, "singleImageSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.singleImageSetWrapper = singleImageSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        // public Travel GetById(Guid? id)
        // {
        //     Travel result = null;
        // 
        //     Travel travel = this.travelSetWrapper.GetById(id);
        //     if (travel != null)
        //     {
        //         result = travel;
        //     }
        // 
        //     return result;
        // }
        // 
        // public TravelTranslationalInfo GetTravelByTitle(string searchTerm)
        // {
        //     // IQueryable<Travel> travels = travelSetWrapper.All;
        //     // var trnaslatedTravels = travels.SelectMany(x => x.TranslatedTravels);
        //     // TravelTranslationalInfo translatedTravelByTitle= trnaslatedTravels.FirstOrDefault(a => a.Title.Contains(searchTerm));
        //     // Travel travel = translatedTravelByTitle.Travel;
        //     return null;
        // }
        // 

        public void Add(SingleImage singleImage)
        {
            singleImageSetWrapper.Add(singleImage);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }

        public IEnumerable<SingleImage> GetLatesSingleImages()
        {
            IQueryable<SingleImage> singleImages = singleImageSetWrapper.All;
            var orderedTravels = singleImages.OrderByDescending(a => a.CreatedOn).Take(4).ToList();
            return orderedTravels;
        }         
    }
}
