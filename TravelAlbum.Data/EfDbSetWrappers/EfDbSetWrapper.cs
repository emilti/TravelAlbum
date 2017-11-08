using Bytes2you.Validation;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace TravelAlbum.Data.EfDbSetWrappers
{
    public class EfDbSetWrapper<T> : IEfDbSetWrapper<T> where T : class
    {
        private readonly TravelAlbumEfDbContext dbContext;
        private readonly IDbSet<T> dbSet;

        public EfDbSetWrapper(TravelAlbumEfDbContext DBContext)
        {
            Guard.WhenArgument(DBContext, "DBContext").IsNull().Throw();

            this.dbContext = DBContext;
            this.dbSet = dbContext.Set<T>();
        }


        public IQueryable<T> All
        {
            get
            {
                return this.dbSet;
            }
        }

        public IQueryable<T> AllWithInclude<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            return this.All.Include(includeExpression);
        }

        public T GetById(Guid? id)
        {
            return this.dbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.dbSet.Attach(entity);
                this.dbSet.Remove(entity);
            }
        }

        // public void SaveChanges()
        // {
        //     this.dbContext.SaveChanges();
        // }

    }

    //public class GenericRepository<T> : IRepository<T> where T : class
    //{
    //    public GenericRepository(ITravelAlbumDbContext context)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
    //        }

    //        this.Context = context;
    //        this.DbSet = this.Context.Set<T>();
    //    }

    //    protected IDbSet<T> DbSet { get; set; }

    //    protected ITravelAlbumDbContext Context { get; set; }

    //    public virtual IQueryable<T> All()
    //    {
    //        return this.DbSet.AsQueryable();
    //    }

    //    public virtual T GetById(object id)
    //    {
    //        return this.DbSet.Find(id);
    //    }

    //    public virtual void Add(T entity)
    //    {
    //        var entry = this.Context.Entry(entity);
    //        if (entry.State != EntityState.Detached)
    //        {
    //            entry.State = EntityState.Added;
    //        }
    //        else
    //        {
    //            this.DbSet.Add(entity);
    //        }
    //    }

    //    public virtual void Update(T entity)
    //    {
    //        var entry = this.Context.Entry(entity);
    //        if (entry.State == EntityState.Detached)
    //        {
    //            this.DbSet.Attach(entity);
    //        }

    //        entry.State = EntityState.Modified;
    //    }

    //    public virtual void Delete(T entity)
    //    {
    //        var entry = this.Context.Entry(entity);
    //        if (entry.State != EntityState.Deleted)
    //        {
    //            entry.State = EntityState.Deleted;
    //        }
    //        else
    //        {
    //            this.DbSet.Attach(entity);
    //            this.DbSet.Remove(entity);
    //        }
    //    }

    //    public virtual void Delete(object id)
    //    {
    //        var entity = this.GetById(id);

    //        if (entity != null)
    //        {
    //            this.Delete(entity);
    //        }
    //    }

    //    public virtual T Attach(T entity)
    //    {
    //        return this.Context.Set<T>().Attach(entity);
    //    }

    //    public virtual void Detach(T entity)
    //    {
    //        var entry = this.Context.Entry(entity);
    //        entry.State = EntityState.Detached;
    //    }

    //    public int SaveChanges()
    //    {
    //        return this.Context.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        this.Context.Dispose();
    //    }
    //}

}
