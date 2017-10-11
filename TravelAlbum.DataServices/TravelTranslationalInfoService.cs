using System;
using Bytes2you.Validation;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using System.Data.Entity;
using TravelAlbum.Data.EfDbSetWrappers;

namespace TravelAlbum.DataServices
{
    public class TravelTranslationalInfoService : ITravelTranslationalInfoService
    {
        private readonly IEfDbSetWrapper<TravelTranslationalInfo> travelTranslationalInfoSetWrapper;

        private readonly IUnitOfWork unitOfWork;

        public TravelTranslationalInfoService(IEfDbSetWrapper<TravelTranslationalInfo> travelTranslationalInfoSetWrapper, IUnitOfWork unitOfWork)
        {
            // Guard.WhenArgument(travelTranslationalInfoSetWrapper, "travelTranslationalInfoSetWrapper").IsNull().Throw();
            // Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            // 
            this.travelTranslationalInfoSetWrapper = travelTranslationalInfoSetWrapper;
            this.unitOfWork = unitOfWork;
        }

        public void Add(TravelTranslationalInfo travelTranslationalInfo)
        {
            travelTranslationalInfoSetWrapper.Add(travelTranslationalInfo);
            
            this.unitOfWork.SaveChanges();
            //this.travelTranslationalInfoSetWrapper.SaveChanges();
        }     
    }
}
