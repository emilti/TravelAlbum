using System;
using System.Collections.Generic;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface IImageService
    {
        // SingleImage GetById(Guid? id);

        // TravelTranslationalInfo GetTravelByTitle(string searchTerm);

        void Add(Image image);

        IEnumerable<Image> GetLatesImages(int pageIndex);

        Image GetById(Guid? id);

        IEnumerable<Image> GetImagesByMountain(List<Guid> mountainsIds, int sorting);

        void SaveChanges();
    }
}
