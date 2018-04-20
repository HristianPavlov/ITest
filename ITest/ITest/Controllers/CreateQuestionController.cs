using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models.QuestionViewModel;
using ITest.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers.Createontrollers
{
    public class CreateQuestionController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IQuestionService questions;
        private readonly UserManager<User> userManager;



        //private readonly 
        public CreateQuestionController(IMappingProvider mapper, IQuestionService questions, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.questions = questions;
            
            this.userManager = userManager;
        }




        [Authorize]
                public IActionResult CreateQ()
        {


            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQ(CreateQuestionViewModel question)
        {

            var model = this.mapper.MapTo<QuestionDTO>(question);
            this.questions.Create(model);

            TempData["Success-Message"] = "You published a new post!";
            return this.RedirectToAction("Index", "Home");
        }


    }
}