using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Data;
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

        public IEnumerable<SingleImage> GetLatesSingleImages(int pageIndex)
        {
            IQueryable<SingleImage> singleImages = singleImageSetWrapper.All;
            var orderedSingleImages = singleImages.OrderByDescending(a => a.CreatedOn).Skip(6 * pageIndex).Take(6).ToList();
            return orderedSingleImages;
        }

        public SingleImage GetById(Guid? id)
        {
            SingleImage result = null;

            SingleImage singleImage = this.singleImageSetWrapper.GetById(id);
            if (singleImage != null)
            {
                result = singleImage;
            }

            return result;
        }

        public IQueryable<SingleImage> GetImagesByMountain(List<Guid> mountainsIds, Sorting sorting)
        {
            if (sorting == Sorting.Ascending)
            {
                var images = this.singleImageSetWrapper.All.Where(a => mountainsIds.Contains((Guid)a.MountainId)).OrderBy(a => a.CreatedOn);
                return images;
            }
            else
            {
                var images = this.singleImageSetWrapper.All.Where(a => mountainsIds.Contains((Guid)a.MountainId)).OrderByDescending(a => a.CreatedOn);
                return images;
            }            
        }
    }
}
