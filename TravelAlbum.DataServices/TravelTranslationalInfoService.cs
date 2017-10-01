using System;
using Bytes2you.Validation;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class TravelTranslationalInfoService : ITravelTranslationalInfoService
    {
        private readonly IEfDbSetWrapper<TravelTranslationalInfo> travelSetWrapper;

        private readonly ITravelAlbumDbContextSaveChanges dbContext;

        public TravelTranslationalInfoService(IEfDbSetWrapper<TravelTranslationalInfo> travelSetWrapper, ITravelAlbumDbContextSaveChanges dbContext)
        {
            Guard.WhenArgument(travelSetWrapper, "travelSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.travelSetWrapper = travelSetWrapper;
            this.dbContext = dbContext;
        }        

        public void Add(TravelTranslationalInfo travelTranslationalInfo)
        {
            travelSetWrapper.Add(travelTranslationalInfo);
            this.dbContext.Commit();
        }     
    }
}
