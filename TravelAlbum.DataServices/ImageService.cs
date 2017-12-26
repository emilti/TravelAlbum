using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class ImageService : IImageService
    {

        private readonly IEfDbSetWrapper<Image> imageSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public ImageService(IEfDbSetWrapper<Image> imageSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(imageSetWrapper, "imageSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.imageSetWrapper = imageSetWrapper;
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

        public void Add(Image image)
        {
            imageSetWrapper.Add(image);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }

        public IEnumerable<Image> GetLatesImages(int pageIndex)
        {
            IQueryable<Image> images = imageSetWrapper.All;
            var orderedImages = images.OrderByDescending(a => a.CreatedOn).Skip(6 * pageIndex).Take(6).ToList();
            return orderedImages;
        }

        public Image GetById(Guid? id)
        {
            Image result = null;

            Image image = this.imageSetWrapper.GetById(id);
            if (image != null)
            {
                result = image;
            }

            return result;
        }

        public IQueryable<Image> GetImagesByMountain(List<Guid> mountainsIds, int sorting)
        {            
            if (sorting == 1)
            {
                var images = this.imageSetWrapper.All.Where(a => mountainsIds.Contains((Guid)a.MountainId)).OrderBy(a => a.CreatedOn);
                return images;
            }            
            else
            {
                var images = this.imageSetWrapper.All.Where(a => mountainsIds.Contains((Guid)a.MountainId)).OrderByDescending(a => a.CreatedOn);
                return images;
            }            
        }
    }
}
