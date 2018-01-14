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

        private readonly IImageTranslationalInfoService imageTranslationalInfoService;

        public ImageService(IEfDbSetWrapper<Image> imageSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges, IImageTranslationalInfoService imageTranslationalInfoService)
        {
            Guard.WhenArgument(imageSetWrapper, "imageSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();
            Guard.WhenArgument(imageTranslationalInfoService, "imageTranslationalInfoService").IsNull().Throw();

            this.imageSetWrapper = imageSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
            this.imageTranslationalInfoService = imageTranslationalInfoService;
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

        public IEnumerable<Image> GetImagesByMountain(List<Guid> mountainsIds, int selectedSorting, string searchedTitle)
        {
            var images = this.imageSetWrapper.All.Where(a => mountainsIds.Contains((Guid)a.MountainId)).ToList();
            var filteredImagesByTitle = images;
            if (searchedTitle != null && searchedTitle != String.Empty)
            {
                filteredImagesByTitle = images.AsQueryable().Where(a => a.TranslatedInfoes.Where(b => b.Title.ToLower().Contains(searchedTitle.ToLower())).ToList().Count > 0).ToList();
            }

            filteredImagesByTitle = filteredImagesByTitle.OrderByWithDirection(a => a.CreatedOn, selectedSorting).ToList();
            return filteredImagesByTitle;                      
        }

        public void SaveChanges()
        {
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }

        public IQueryable<Image> All()
        {
            return this.imageSetWrapper.All;
        }
    }
}
