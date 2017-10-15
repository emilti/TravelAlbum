using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TravelAlbum.Models;

namespace TravelAlbum.Data.Contracts
{
    public interface ITravelAlbumEfDbContextSaveChanges
    {        
        int SaveChanges();
    }
}
