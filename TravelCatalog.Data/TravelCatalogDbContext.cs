using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCatalog.Models;

namespace TravelCatalog.Data
{
    public class TravelCatalogDbContext : DbContext
    {

        public TravelCatalogDbContext()
            : base("TravelCatalogDb")
        {
        }

        public virtual IDbSet<Travel> Travels { get; set; }
    }
}
