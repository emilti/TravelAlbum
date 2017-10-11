using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TravelAlbum.Models;

namespace TravelAlbum.Data.Contracts
{
    public interface IUnitOfWork
    {
        //IDbSet<Travel> Travels { get; set; }

        //IDbSet<TravelTranslationalInfo> TranslatedTravels { get; set; }

        //IDbSet<TravelImage> Images { get; set; }

        //IDbSet<Mountain> Mountains { get; set; }       


        int SaveChanges();

        //IDbSet<T> Set<T>() where T : class;

        //DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
          //where TEntity : class;
    }
}
