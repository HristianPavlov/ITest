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
    public class CreateController : Controller
    {
        private readonly ICategoriesService categories;
        private readonly IMappingProvider mapper;

        public CreateController(ICategoriesService categories, IMappingProvider mapper)
        {
            this.categories = categories;
            this.mapper = mapper;
        }
        public IActionResult CreateCategoryForm()
        {
            return View();
        }
        public IActionResult CreateCategory(CreateCategoryViewModel cattegoryToAdd)
        {
            if (this.ModelState.IsValid)
            {
                var dto = this.mapper.MapTo<CategoryDTO>(cattegoryToAdd);

                this.categories.Add(dto);

                TempData["Success-Message"] = "You published a new post!";
                return this.RedirectToAction("Index", "Home");
            }
            return View("CreateCategoryForm");
        }
        public IActionResult CreateTest()
        {
            return View();
        }
    }
}