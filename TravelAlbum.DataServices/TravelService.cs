using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAlbum.Data.Contracts;
using TravelAlbum.Data.EfDbSetWrappers;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class TravelService : ITravelService
    {
        private readonly IEfDbSetWrapper<Travel> travelSetWrapper;

        private readonly IUnitOfWork unitOfWork;

        public TravelService(EfDbSetWrapper<Travel> travelSetWrapper, IUnitOfWork unitOfWork)
        {
            //Guard.WhenArgument(travelSetWrapper, "travelSetWrapper").IsNull().Throw();
            //Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.travelSetWrapper = travelSetWrapper;
            this.unitOfWork = unitOfWork;
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
            this.unitOfWork.SaveChanges();
        }
    }
}
