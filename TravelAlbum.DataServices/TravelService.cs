using System;
using TravelAlbum.Data.Contracts;
using TravelAlbum.Data.EfDbSetWrappers;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class TravelService : ITravelService
    {
        private readonly IEfDbSetWrapper<Travel> travelSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public TravelService(EfDbSetWrapper<Travel> travelSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            //Guard.WhenArgument(travelSetWrapper, "travelSetWrapper").IsNull().Throw();
            //Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.travelSetWrapper = travelSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public Travel GetById(Guid id)
        {
            Travel result = null;

            Travel travel = this.travelSetWrapper.GetById(id);
            if (travel != null)
            {
                result = travel;
            }


            return result;
        }

        public TravelTranslationalInfo GetTravelByTitle(string searchTerm)
        {
            // IQueryable<Travel> travels = travelSetWrapper.All;
            // var trnaslatedTravels = travels.SelectMany(x => x.TranslatedTravels);
            // TravelTranslationalInfo translatedTravelByTitle= trnaslatedTravels.FirstOrDefault(a => a.Title.Contains(searchTerm));
            // Travel travel = translatedTravelByTitle.Travel;
            return null;
        }

        public void Add(Travel travel)
        {
            travelSetWrapper.Add(travel);
            //this.dbContext.SaveChanges();
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }
    }
}
