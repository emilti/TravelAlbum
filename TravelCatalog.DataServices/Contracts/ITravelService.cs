using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCatalog.Models;

namespace TravelCatalog.DataServices.Contracts
{
   
        public interface ITravelService
        {
            Travel GetById(Guid? id);

            Travel GetTravelByTitle(string searchTerm);
        
    }
}
