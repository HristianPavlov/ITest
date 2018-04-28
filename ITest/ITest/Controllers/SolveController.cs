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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    public class SolveController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IUserTestsService userTestsService;
        private readonly ICategoriesService categoriesService;
        private readonly ITestService testsService;
        private readonly UserService userService;

        public SolveController(IMappingProvider mapper,
            IUserTestsService userTestsService,
            ICategoriesService categoriesService,
            ITestService testsService,
            UserService userService)
        {
            this.mapper = mapper;
            this.userTestsService = userTestsService;
            this.categoriesService = categoriesService;
            this.testsService = testsService;
            this.userService = userService;
        }
        public IActionResult CategoryDone()
        {
            return View();
        }
        public IActionResult TimeUpNotSubmitted()
        {
            return View();
        }
        public IActionResult SubmittingLate()
        {
            return View();
        }
        public IActionResult ShowTest(string id)
        //beneath is the category name not id !!
        {
            var category = id;
            var userId = userService.GetLoggedUserId(this.User);

            if (userTestsService.UserStartedTest(userId, category))
            {
                var testId = userTestsService.GetTestIdFromUserIdAndCategory(userId, category);
                var test = testsService.GetTestById(testId);

                var testView = mapper.MapTo<SolveTestViewModel>(test);
                var testAllowedTime = double.Parse(test.TimeInMinutes.ToString());
                var startedTime = userTestsService.StartedTestCreationTime(userId, category);
                //fix this to have is test submitted
                if (userTestsService.TestIsSubmitted(userId, category))
                {
                    return this.RedirectToAction("CategoryDone", "Solve");
                }
                var endTime = startedTime.Value.AddMinutes(testAllowedTime);
                var reaminingTime = Math.Round((endTime - DateTime.Now).TotalSeconds);

                testView.Category = category;
                testView.RemainingTime = int.Parse(reaminingTime.ToString());

                if (reaminingTime < 0)
                {
                    return this.RedirectToAction("TimeUpNotSubmitted", "Solve");
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
                userTestsService.SaveTest(saveThisTestCreation);
                //added the up

                testViewModel.StorageOfAnswers = new List<string>();
                for (int i = 0; i < testViewModel.Questions.Count; i++)
                {
                    testViewModel.StorageOfAnswers.Add("No Answer");
                }

                testViewModel.Category = category;
                testViewModel.CreatedOn = DateTime.Now;
                testViewModel.RemainingTime =
                    Convert.ToInt32(((DateTime.Now.AddMinutes(randomTest.TimeInMinutes) - DateTime.Now).TotalSeconds)/*.ToString()*/);
                return View(testViewModel);
            }
        }

        [HttpPost]
        public IActionResult PublishAnswers(SolveTestViewModel answers)
        {
            if (ModelState.IsValid)
            {
                //make it here to not be publishable if remaining time is incorrect
                var userId = userService.GetLoggedUserId(this.User);
                var startedTime = userTestsService.StartedTestCreationTime(userId, answers.Category);
                var countDown = testsService.GetTestCountDownByTestId(answers.Id);
                //giving the system 1 minute buffer for the test to submit later than the predicted time
                var timeRemain = (startedTime.Value.AddMinutes(countDown).AddMinutes(1) - DateTime.Now).TotalSeconds;
                var executionTime = (timeRemain - 60)/60;
                if (timeRemain < 0)
                {
                    return this.RedirectToAction("SubmittingLate", "Solve");
                }
                var completedTest = mapper.MapTo<UserTestsDTO>(answers);

                completedTest.TestId = completedTest.Id;
                completedTest.UserId = userId;
                completedTest.ExecutionTime = Math.Round(executionTime,2);

                userTestsService.Publish(completedTest);
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}