﻿using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface ISingleImageService
    {
        // SingleImage GetById(Guid? id);

        // TravelTranslationalInfo GetTravelByTitle(string searchTerm);

        void Add(SingleImage singleImage);

        IEnumerable<SingleImage> GetLatesSingleImages();
    }
}
