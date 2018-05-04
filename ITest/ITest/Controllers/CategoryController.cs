using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Data;
using ITest.Data.Models;
using ITest.DTO;
using ITest.Infrastructure.Providers;
using ITest.Models.CategoryViewModels;
using ITest.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUserTestsService userTestsService;
        private readonly IUserService userService;
        private readonly ICategoriesService categoriesService;
        private readonly IMappingProvider mapper;

        public CategoryController(IUserTestsService userTestsService, IUserService userService, ICategoriesService categoriesService, IMappingProvider mapper)
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



        public IActionResult ShowCategories()
        {
            var model = new CategoriesViewModel();
            var categories = this.categoriesService.GetAllCategories();
            var categoriesViewModel = this.mapper.ProjectTo<CategoryViewModel>(categories.AsQueryable()).ToHashSet();

            var allStartedUserTests = userTestsService.GetCurrentUserTests(userService.GetLoggedUserId(User));
            var allStartedCategoriesView = mapper.ProjectTo<CategoryViewModel>(allStartedUserTests.AsQueryable()).ToHashSet();


            foreach (var item in categoriesViewModel)
            {
                if (!allStartedCategoriesView.Any(x => x.Category == item.Category))
                {
                    allStartedCategoriesView.Add(item);
                }
            }


            model.AllCategories = allStartedCategoriesView.OrderBy(x => (int)(x.CategoryState));
            return View(model);
        }
    }
}