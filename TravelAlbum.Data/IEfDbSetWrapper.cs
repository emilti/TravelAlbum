﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace TravelAlbum.Data
{
    public interface IEfDbSetWrapper<T>
  where T : class
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllWithInclude<TProperty>(Expression<Func<T, TProperty>> includeExpression);

        T GetById(Object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        // void SaveChanges();
    }
}
