using System.Data.Entity;
using TravelCatalog.Models;

namespace TravelCatalog.Data
{
    public class TravelCatalogEfDbContext : DbContext// , ILiveDemoEfDbContextSaveChanges
    {

        public TravelCatalogEfDbContext()
            : base("TravelCatalog")
        {
        }


        public static TravelCatalogEfDbContext Create()
        {
            return new TravelCatalogEfDbContext();
        }

        public virtual IDbSet<Travel> Travels { get; set; }

        public new IDbSet<T> Set<T>()
         where T : class
        {
            return base.Set<T>();
        }

    }
}
