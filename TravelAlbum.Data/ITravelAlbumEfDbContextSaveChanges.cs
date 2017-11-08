using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAlbum.Data
{
    public interface ITravelAlbumEfDbContextSaveChanges
    {
        int SaveChanges();
    }
}
