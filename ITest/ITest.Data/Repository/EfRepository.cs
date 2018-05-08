using ITest.Data.Models.Abstracts;
using ITest.Data.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITest.Data.Repository
{
  public  class EfRepository<T> : IRepository<T> where T : class, IDeletable
    {
        private readonly ITestDbContext context;
        private readonly IRepoTimeProvider dateTime;

        public EfRepository(ITestDbContext context, IRepoTimeProvider dateTime)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        public IQueryable<T> All
        {
            get
            {
                return this.context.Set<T>().Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        public void Add(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = dateTime.GetDateTimeNow();

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void RealDelete(T entity)
        {
        
            if (context.Entry(entity).State == EntityState.Detached)
            {
                this.context.Attach(entity);
            }
            this.context.Remove(entity);
        
    }

        public void Update(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
