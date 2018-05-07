using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITest.Models;

namespace ITest.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return this.RedirectToAction("ShowResults", "Results", new { area = "Admin" });
            }
            else if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("ShowCategories", "Category", new { area = "User" });
            }
            else
            {
                return this.RedirectToAction("Authorize", "Account");
            }

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
