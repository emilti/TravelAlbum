using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCatalog.Data.Contracts;
using TravelCatalog.DataServices.Contracts;
using TravelCatalog.Models;

namespace TravelCatalog.DataServices
{
    public class TravelService : ITravelService
    {
        private readonly IEfDbSetWrapper<Travel> travelSetWrapper;

        private readonly ITravelCatalogDbContextSaveChanges dbContext;

        public TravelService(IEfDbSetWrapper<Travel> travelSetWrapper, ITravelCatalogDbContextSaveChanges dbContext)
        {
            Guard.WhenArgument(travelSetWrapper, "travelSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.travelSetWrapper = travelSetWrapper;
            this.dbContext = dbContext;
        }

        public Travel GetById(Guid? id)
        {
            Travel result = null;

            if (id.HasValue)
            {
                Travel travel = this.travelSetWrapper.GetById(id.Value);
                if (travel != null)
                {
                    result = travel;
                }
            }

            return result;
        }

        public Travel GetTravelByTitle(string searchTerm)
        {
            IQueryable<Travel> travels = travelSetWrapper.All;
            Travel travelByTitle= travels.FirstOrDefault(a => a.Title.Contains(searchTerm));
            return travelByTitle;
        }
    }
}
