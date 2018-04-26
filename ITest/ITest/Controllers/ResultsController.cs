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
        private readonly ITestRandomService testsService;

        public ResultsController(IMappingProvider mapper,
            ITestRandomService testsService)
        {
            this.mapper = mapper;
            this.testsService = testsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowResults()
        {
            var model = new ResultBagViewModel();
            model.resultBag = mapper.ProjectTo<ResultsViewModel>(testsService.GetAllUserTests());

            return View(model);
        }
    }
}