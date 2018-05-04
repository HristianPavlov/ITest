using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Infrastructure.Providers;
using ITest.Models.ResultsViewModels;
using ITest.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IUserTestsService userTestsService;

        public ResultsController(IMappingProvider mapper,
                                 IUserTestsService userTestsService)
        {
            this.mapper = mapper;
            this.userTestsService = userTestsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowResults()
        {
            var model = new ResultBagViewModel();

            var userTests = this.userTestsService.GetAllUserTests();
            model.ResultBag = mapper.ProjectTo<ResultsViewModel>(userTests.AsQueryable());


            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RecalculateTests()
        {
            //this.userTestsService.RecalculateAllTestsScore();
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        //[HttpPost]
        public IActionResult DetailedSolution(string userEmail, Guid testId)
        {
            var detailsDto = this.userTestsService.GetDetailedSolution(userEmail, testId);

            //var modelDto = this.userTestsService.GetUserTest(userEmail, testId);
            //if (!modelDto.StorageOfAnswers.Any())
            //{
            //    return this.RedirectToAction("ShowResults", "Results");
            //}
            var viewModel = this.mapper.MapTo<DetailedTestViewModel>(detailsDto);
            return View(viewModel);
        }

    }
}