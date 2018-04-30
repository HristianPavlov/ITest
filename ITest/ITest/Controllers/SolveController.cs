using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.DTO;
using ITest.Infrastructure.CustomExceptions;
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
        private readonly IUserService userService;

        public SolveController(IMappingProvider mapper,
            IUserTestsService userTestsService,
            ICategoriesService categoriesService,
            ITestService testsService,
            IUserService userService)
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
            try
            {
                var category = id;
                var userId = userService.GetLoggedUserId(this.User);
                var correctUserTestDto = userTestsService.GetCorrectSolveTest(userId, category);
                var correctUserTest = mapper.MapTo<SolveTestViewModel>(correctUserTestDto);
                return View(correctUserTest);
            }
            catch (CategoryDoneException)
            {
                return this.RedirectToAction("CategoryDone", "Solve");
            }
            catch (TimeUpNeverSubmittedException)
            {
                return this.RedirectToAction("TimeUpNotSubmitted", "Solve");
            }

        }
        [HttpPost]
        public IActionResult PublishAnswers(SolveTestViewModel answers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = userService.GetLoggedUserId(this.User);
                    var solveTestDto = mapper.MapTo<SolveTestDTO>(answers);
                    userTestsService.ValidateAndAdd(solveTestDto, userId);
                }
                catch (SubmittingLateException)
                {
                    RedirectToAction("SubmittingLate", "Solve");
                }
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}