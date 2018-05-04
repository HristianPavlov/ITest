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
        private readonly IUserTestAnswersService utaService;

        public ResultsController(IMappingProvider mapper,
                                 IUserTestsService userTestsService,
                                 IUserTestAnswersService utaService)
        {
            this.mapper = mapper;
            this.userTestsService = userTestsService;
            this.utaService = utaService;
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult ShowResults()
        {
            var model = new ResultBagViewModel();

            var userTests = this.userTestsService.GetAllUserTests();
            model.ResultBag = mapper.ProjectTo<ResultsViewModel>(userTests.AsQueryable());


            return View(model);
        }
        //should be deleted (its only used for test purposes)
        //[Authorize(Roles = "Admin")]
        //public IActionResult RecalculateTests()
        //{
        //    this.utaService.RecalculateAllTakenTestsWithId(Guid.Parse("CFC91AB1-E2C0-48CA-101C-08D5B1B805CE"));
        //    return this.RedirectToAction("Index", "Home");
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        public IActionResult DetailedSolution(string userEmail, Guid testId)
        {
            var detailsDto = this.userTestsService.GetDetailedSolution(userEmail, testId);
            var viewModel = this.mapper.MapTo<DetailedTestViewModel>(detailsDto);
            return View(viewModel);
        }

    }
}