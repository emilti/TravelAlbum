using TravelAlbum.Data.Contracts;

namespace TravelAlbum.Data
{
    public class TravelAlbumEfDbContextSaveChanges : ITravelAlbumEfDbContextSaveChanges
    {
        private readonly TravelAlbumEfDbContext travelAlbumDbContext;
       
        public TravelAlbumEfDbContextSaveChanges(TravelAlbumEfDbContext travelAlbumDbContext)
        {
            this.travelAlbumDbContext = travelAlbumDbContext;
        }


        public int SaveChanges()
        {
            return travelAlbumDbContext.SaveChanges();
        }
    }
}
