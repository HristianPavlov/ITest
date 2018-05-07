using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Areas.Admin.Models.EditViewModels;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models;
using ITest.Models.ResultsViewModels;
using ITest.Models.TestRealBagViewModel;
using ITest.Services.Data;
using ITest.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EditTestController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ICreateTestService createTestService;
        private readonly ITestService testService;

        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IUserTestsService userTestsService;
        private readonly ICategoriesService categories;
        private readonly IUserTestAnswersService utaService;

        public EditTestController(IMappingProvider mapper, ICreateTestService createTestService
            , UserManager<User> userManager, IUserService userService
            , IUserTestsService userTestsService
            , ITestService testService, ICategoriesService categories
            , IUserTestAnswersService utaService
            )
        {
            this.mapper = mapper;
            this.createTestService = createTestService;
            this.userManager = userManager;
            this.userService = userService;
            this.userTestsService = userTestsService;
            this.testService = testService;
            this.categories = categories;
            this.utaService = utaService;
        }
        public IActionResult SearchTest()
        {

            var model = new TestRealBagViewModel();

            var userTests = this.testService.GetAllTestsWithOutStuffInIttEditDTO();
            model.ResultBag = mapper.ProjectTo<TestEditDTO>(userTests.AsQueryable());
            return View(model);
        }
        public IActionResult EditTest(string id)
        {
            var testDto = this.testService.GetTestByNameEditDTO(id);



            return View(testDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTest(TestEditDTO test)
        {
            this.createTestService.Update(test);
            
            return this.RedirectToAction("ShowResults", "Results");
        }

        public IActionResult EditPublishedTest(string id)
        {
            var testDto = this.testService.GetTestByNameEditDTO(id);


            return View("EditPublishedTest", testDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPublishedTest(TestEditDTO test)
        {
            this.createTestService.PublishedUpdate(test);
            this.utaService.RecalculateAllTakenTestsWithName(test.Name);
            return this.RedirectToAction("ShowResults", "Results");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTest(string testName)
        {
            this.createTestService.DeleteTest(testName);

            return this.Json(true);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DisableTest(string testName)
        {

            this.createTestService.Disable(testName);

            var model = new TestRealBagViewModel();

            var userTests = this.testService.GetAllTestsWithOutStuffInIttEditDTO();
            model.ResultBag = mapper.ProjectTo<TestEditDTO>(userTests.AsQueryable());

            return View("SearchTest", model);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublishTest(string testName)
        {

            this.createTestService.Publish(testName);
            var model = new TestRealBagViewModel();

            var userTests = this.testService.GetAllTestsWithOutStuffInIttEditDTO();
            model.ResultBag = mapper.ProjectTo<TestEditDTO>(userTests.AsQueryable());

            return View("SearchTest", model);
        }





    }

}