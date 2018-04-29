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

            model.ResultBag = mapper.ProjectTo<ResultsViewModel>(userTestsService.GetAllUserTests().AsQueryable());


            return View(model);
        }
    }
}