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
    public class CreateTestsController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IQuestionService questionsService;
        private readonly ICreateTestService createTestService;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public CreateTestsController(IMappingProvider mapper, IQuestionService questionsService,  ICreateTestService createTestService, UserManager<User> userManager, IUserService userService)
        {
            this.mapper = mapper;
            this.questionsService = questionsService;
            this.createTestService = createTestService;
            this.userManager = userManager;
            this.userService = userService;
        }

        
      
        public IActionResult CreateNewTest()
        {
            //var model = new CreateTestViewModel()
            //{model

            //};
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewTest(CreateTestViewModel question)
        {

            var model = this.mapper.MapTo<TestDTO>(question);
           // model.AuthorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.AuthorId = userService.GetLoggedUserId(this.User);
           
            this.createTestService.Create(model);

            TempData["Success-Message"] = "You published a new post!";
            return this.View(); /*this.RedirectToAction("Index", "Home");*/
        }
    }
}
