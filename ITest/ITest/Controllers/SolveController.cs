using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models.AnswerViewModels;
using ITest.Models.CategoryViewModels;
using ITest.Models.TestViewModels;
using ITest.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    public class SolveController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ICategoriesService categoriesService;
        private readonly ITestRandomService testsService;
        private readonly UserService userService;

        public SolveController(IMappingProvider mapper, ICategoriesService categoriesService, ITestRandomService testsService, UserService userService)
        {
            this.mapper = mapper;
            this.categoriesService = categoriesService;
            this.testsService = testsService;
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
            //testViewModel.Storage = new AnswerStorageViewModel();
            testViewModel.StorageOfAnswers = new List<string>();
            for (int i = 0; i < testViewModel.Questions.Count; i++)
            {
                testViewModel.StorageOfAnswers.Add("No Answer");
            }


            return View(testViewModel);
        }

        [HttpPost]
        public IActionResult PublishAnswers(SolveTestViewModel answers)
        {
            if (ModelState.IsValid)
            {
                var completedTest = mapper.MapTo<UserTestsDTO>(answers);
                var userId = this.userService.GetLoggedUserId(this.User);
                //fix this in the view
                completedTest.TestId = completedTest.Id;
                completedTest.UserId = userId;
                testsService.Publish(completedTest);
            }
            return View(answers);
        }
    }
}