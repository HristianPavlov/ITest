using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITest.Data.Repository
{
    public interface IRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        
       


    }
}
