using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models.CategoryViewModels;
using ITest.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "User")]
    public class CreateCategoryController : Controller
    {
        private readonly IUserTestsService userTestsService;
        private readonly IUserService userService;
        private readonly ICategoriesService categoriesService;
        private readonly IMappingProvider mapper;

        public CreateCategoryController(IUserTestsService userTestsService, IUserService userService, ICategoriesService categoriesService, IMappingProvider mapper)
        {
            this.userTestsService = userTestsService;
            this.userService = userService;
            this.categoriesService = categoriesService;
            this.mapper = mapper;
        }
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel cattegoryToAdd)
        {
            if (this.ModelState.IsValid)
            {
                var dto = this.mapper.MapTo<CategoryDTO>(cattegoryToAdd);

                this.categoriesService.Add(dto);

                TempData["Success-Message"] = "You published a new post!";
                return this.RedirectToAction("Index", "Home");
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}