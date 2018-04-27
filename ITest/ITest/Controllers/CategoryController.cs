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
        private readonly ICategoriesService categoriesService;
        private readonly IMappingProvider mapper;

        public CategoryController(ICategoriesService categoriesService, IMappingProvider mapper)
        {
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
            model.AllCategories = this.mapper.ProjectTo<CategoryViewModel>(categories).ToList();
            return View(model);
        }
    }
}