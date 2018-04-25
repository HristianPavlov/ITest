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
        //add interface to userService

        public SolveController(IMappingProvider mapper,
            ICategoriesService categoriesService,
            ITestRandomService testsService,
            UserService userService)
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
        public IActionResult CategoryDone()
        {
            return View();
        }

        public IActionResult GenerateTest(string id)
        {
            return Json(Url.Action("ShowTest/" + id));
        }

        public IActionResult ShowTest(string id)
        //beneath is the category name not id !!
        {
            var category = id;
            var userId = userService.GetLoggedUserId(this.User);

            if (testsService.UserStartedTest(userId, category))
            {
                var testId = testsService.GetTestIdFromUserIdAndCategory(userId, category);
                var test = testsService.GetTestById(testId);

                var testView = mapper.MapTo<SolveTestViewModel>(test);
                var testAllowedTime = double.Parse(test.TimeInMinutes.ToString());
                var startedTime = testsService.StartedTestCreationTime(userId, category);
                if (startedTime == null)
                {
                    return this.RedirectToAction("CategoryDone", "Solve");
                }
                var endTime = startedTime.Value.AddMinutes(testAllowedTime);
                var reaminingTime = Math.Round((endTime - DateTime.Now).TotalSeconds);

                testView.Category = category;
                testView.RemainingTime = int.Parse(reaminingTime.ToString());

                if (reaminingTime<0)
                {
                    return this.RedirectToAction("CategoryDone", "Solve");
                }
                
                testView.StorageOfAnswers = new List<string>();
                for (int i = 0; i < testView.Questions.Count; i++)
                {
                    testView.StorageOfAnswers.Add("No Answer");
                }
                return View(testView);
            }
            else
            {
                var categoryId = categoriesService.GetIdByCategoryName(category);
                var randomTest = testsService.GetRandomTestFromCategory(categoryId);
                var testViewModel = mapper.MapTo<SolveTestViewModel>(randomTest);
                // add the test in base on creation
                var saveThisTestCreation = new UserTestsDTO
                {
                    UserId = userId,
                    Category = category,
                    TestId = randomTest.Id
                };
                testsService.SaveTest(saveThisTestCreation);
                //added the up

                testViewModel.StorageOfAnswers = new List<string>();
                for (int i = 0; i < testViewModel.Questions.Count; i++)
                {
                    testViewModel.StorageOfAnswers.Add("No Answer");
                }

                testViewModel.Category = category;
                testViewModel.CreatedOn = DateTime.Now;
                testViewModel.RemainingTime =
                    Convert.ToInt32(((DateTime.Now.AddMinutes(randomTest.TimeInMinutes) - DateTime.Now).TotalSeconds).ToString());
                return View(testViewModel);
            }
        }

        [HttpPost]
        public IActionResult PublishAnswers(SolveTestViewModel answers)
        {
            if (ModelState.IsValid)
            {
                //crap start here
                var userId = userService.GetLoggedUserId(this.User);
                //var startedTime = testsService.StartedTestCreationTime(userId, answers.Category);

                var completedTest = mapper.MapTo<UserTestsDTO>(answers);
                //fix this in the view
                completedTest.TestId = completedTest.Id;
                completedTest.UserId = userId;

                testsService.Publish(completedTest);
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}