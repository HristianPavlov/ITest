//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ITest.Data.Models;
//using ITest.DTO;
//using ITest.Infrastructure.Providers;
//using ITest.Models;
//using ITest.Models.QuestionViewModel;
//using ITest.Services.Data.Contracts;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace ITest.Controllers.Createontrollers
//{
//    public class CreateQuestionController : Controller
//    {
//        private readonly IMappingProvider mapper;
//        private readonly IQuestionService questions;
//        private readonly UserManager<User> userManager;
//        public CreateQuestionController(IMappingProvider mapper, IQuestionService questions, UserManager<User> userManager)
//        {
//            this.mapper = mapper;
//            this.questions = questions;

//            this.userManager = userManager;
//        }

//        [Authorize(Roles = "Admin")]
//        public IActionResult CreateQ()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Authorize(Roles = "Admin")]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateQ(CreateTestViewModel question)
//        {

//            var model = this.mapper.MapTo<TestDTO>(question);
//            this.test.Create(model);

//            TempData["Success-Message"] = "You published a new post!";
//            return this.RedirectToAction("Index", "Home");
//        }
//    }
//}