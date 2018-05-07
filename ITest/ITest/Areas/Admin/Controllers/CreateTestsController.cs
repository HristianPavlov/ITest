using ITest.Data.Models;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models;
using ITest.Services.Data;
using ITest.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ITest.Controllers.Createontrollers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CreateTestsController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IQuestionService questionsService;
        private readonly ICreateTestService createTestService;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly ICategoriesService categoriesServices;
        public CreateTestsController(IMappingProvider mapper, IQuestionService questionsService,
            ICreateTestService createTestService, UserManager<User> userManager,
            IUserService userService, ICategoriesService categoriesServices)
        {
            this.mapper = mapper;
            this.questionsService = questionsService;
            this.createTestService = createTestService;
            this.userManager = userManager;
            this.userService = userService;
            this.categoriesServices = categoriesServices;
        }
        public IActionResult CreateNewTest()
        {
            //var model = new CreateTestViewModel()
            //{model

            //};
            var model = new CreateTestViewModel()
            {
                CategoryNames = this.categoriesServices.GetAllCategoriesNames().ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewTest(CreateTestViewModel test)
        {

            var categoryID = this.categoriesServices.GetIdByCategoryName(test.CategoryName);

            var model = this.mapper.MapTo<TestDTO>(test);
            model.CategoryId = categoryID;
            model.AuthorId = userService.GetLoggedUserId(this.User);

            this.createTestService.Create(model);

            //TempData["Success-Message"] = "You published a new post!";
            return this.RedirectToAction("ShowResults", "Results");
        }
    }
}
