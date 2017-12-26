using System;
using System.Collections.Generic;
using System.Linq;
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

        IQueryable<Image> GetImagesByMountain(List<Guid> mountainsIds, int sorting);
    }
}
