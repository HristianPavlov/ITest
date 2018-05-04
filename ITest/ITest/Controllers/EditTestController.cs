using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models;
using ITest.Models.ResultsViewModels;
using ITest.Models.TestRealBagViewModel;
using ITest.Services.Data;
using ITest.Services.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    public class EditTestController : Controller
    {
        private readonly IMappingProvider mapper;

        private readonly ICreateTestService createTestService;
        private readonly ITestService testService;

        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IUserTestsService userTestsService;
        private readonly ICategoriesService categories;

        public EditTestController(IMappingProvider mapper, ICreateTestService createTestService
            , UserManager<User> userManager, IUserService userService
            , IUserTestsService userTestsService
            , ITestService testService, ICategoriesService categories
            )
        {
            this.mapper = mapper;
            this.createTestService = createTestService;
            this.userManager = userManager;
            this.userService = userService;
            this.userTestsService = userTestsService;
            this.testService = testService;
            this.categories = categories;

        }


        //[HttpGet]
        public IActionResult SearchTest()
        {
            //categories.Update();
            
            var model = new TestRealBagViewModel();

            var userTests = this.testService.GetAllTestsWithOutStuffInIttEditDTO();
            model.ResultBag = mapper.ProjectTo<TestEditDTO>(userTests.AsQueryable());
            return View(model);
        }


        
        
        public IActionResult EditTest(string id)
        {
            var testDto = this.testService.GetTestByNameEditDTO(id);
            
            //TestViewModel test = this.mapper.MapTo<TestViewModel>(testDto);

            return View("EditTest", testDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTest(TestEditDTO test)
        {


            
           //var testDto = this.testService.GetTestByNameEditDTO(test.Name);

            //var model = this.mapper.MapTo<TestEditDTO>(test);
            //model.Id = testDto.Id;
            //model.Category = testDto.Category;
            //model.CategoryId = testDto.CategoryId;
            //model.AuthorId = testDto.AuthorId;


            this.createTestService.Update(test);

           // TempData["Success-Message"] = "You published a new post!";
            return this.RedirectToAction("Index", "Home");
        }



    }
}