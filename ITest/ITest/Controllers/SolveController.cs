using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Infrastructure.Providers;
using ITest.Models.CategoryViewModels;
using ITest.Models.TestViewModels;
using ITest.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    public class SolveController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ICategoriesService categoriesService;

        public SolveController(IMappingProvider mapper, ICategoriesService categoriesService)
        {
            this.mapper = mapper;
            this.categoriesService = categoriesService;
        }
        public IActionResult SolveTests()
        {
            var model = new CategoriesViewModel();

            var categories = this.categoriesService.GetAllCategories();
            model.AllCategories = this.mapper.ProjectTo<CategoryViewModel>(categories).ToList();
            return View(model);
        }

        public IActionResult GenerateTest(string category)
        {
            var model = new SolveTestViewModel();

            
            return View(model);
        }
    }
}