using ITest.Data.Models;
using ITest.Data.Models.Enums;
using ITest.Data.Repository;
using ITest.Data.UnitOfWork;
using ITest.DTO;
using ITest.DTO.Enums;
using ITest.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITest.Services.Data
{
    public class CategoriesService : ICategoriesService
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
        public Guid GetIdByCategoryName(string name)
        {
            return categories.All.First(cat => cat.Name == name).Id;
        }
        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = this.categories.All.Include(x => x.Tests);

            var categoriesDto = mapper.ProjectTo<CategoryDTO>(categories).ToList();

            foreach (var item in categoriesDto)
            {
                if (item.Tests.Count > 0 && item.Tests.Any(t => t.Status == TestStatus.Published && !t.IsDeleted))
                {
                    item.CategoryState = UserTestState.Start;
                }
                else
                {
                    item.CategoryState = UserTestState.CategoryEmpty;

                }
            }
            return categoriesDto;
        }


        public void Update()
        {
            var cat = categories.All.Where(x => x.Id == 3).First();
            cat.Name = "Fuck you";
            categories.Update(cat);
            this.saver.SaveChanges();
        }
    }
}
