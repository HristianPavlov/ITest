using ITest.Data.Models;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public class CategoriesService:ICategoriesService
    {
        private readonly IMappingProvider mapper;
        private readonly IRepository<Category> categories;
        private readonly ISaver saver;

        public CategoriesService(IMappingProvider mapper, IRepository<Category> categories, ISaver saver)
        {
            this.mapper = mapper;
            this.categories = categories;
            this.saver = saver;
        }

       

        public void Add(CategoryDTO dto)
        {
            var model = this.mapper.MapTo<Category>(dto);
            this.categories.Add(model);
            this.saver.SaveChanges();
        }

        
    }
}
