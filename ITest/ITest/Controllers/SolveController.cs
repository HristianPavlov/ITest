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
        private readonly ITestRandomService testsService;

        public SolveController(IMappingProvider mapper, ICategoriesService categoriesService, ITestRandomService testsService)
        {
            this.mapper = mapper;
            this.categoriesService = categoriesService;
            this.testsService = testsService;
        }
        public IActionResult SolveTests()
        {
            var model = new CategoriesViewModel();

            var categories = this.categoriesService.GetAllCategories();
            model.AllCategories = this.mapper.ProjectTo<CategoryViewModel>(categories).ToList();
            return View(model);
        }

        public IActionResult GenerateTest(string id)
        {
            return Json(Url.Action("ShowTest/" + id));
        }

        public IActionResult ShowTest(string id)
        {
            var categoryId = categoriesService.GetIdByCategoryName(id);
            var randomTest = testsService.GetRandomTestFromCategory(categoryId);
            var testViewModel = mapper.MapTo<SolveTestViewModel>(randomTest);

            return View(testViewModel);
        }
    }
}