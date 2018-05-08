using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Areas.Admin.Models.TestAndResultBag;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models.ResultsViewModels;
using ITest.Models.TestRealBagViewModel;
using ITest.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ResultsController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IUserTestsService userTestsService;
        private readonly IUserTestAnswersService utaService;
        private readonly ITestService testService;

        public ResultsController(IMappingProvider mapper,
                                 IUserTestsService userTestsService,
                                 IUserTestAnswersService utaService,
                                 ITestService testService)
        {
            this.mapper = mapper;
            this.userTestsService = userTestsService;
            this.utaService = utaService;
            this.testService = testService;
        }
        public IActionResult ShowResults()
        {
            var modelTests = new TestRealBagViewModel();
            var firstUserTests = this.testService.GetAllTestsWithOutStuffInIttEditDTO();
            modelTests.ResultBag = mapper.ProjectTo<TestEditDTO>(firstUserTests.AsQueryable());

            var modelResults = new ResultBagViewModel();
            var secondUserTests = this.userTestsService.GetAllUserTests();
            modelResults.ResultBag = mapper.ProjectTo<ResultsViewModel>(secondUserTests.AsQueryable());

            var model = new TestResultBag();
            model.Test = modelTests;
            model.Results = modelResults;



            return View(model);
        }
        //[HttpPost]
        public IActionResult DetailedSolution(string userEmail, Guid testId)
        {
            var detailsDto = this.userTestsService.GetDetailedSolution(userEmail, testId);
            var viewModel = this.mapper.MapTo<DetailedTestViewModel>(detailsDto);
            return View(viewModel);
        }

    }
}