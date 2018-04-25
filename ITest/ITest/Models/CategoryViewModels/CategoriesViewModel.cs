using ITest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.CategoryViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<CategoryViewModel> AllCategories { get; set; }
    }
}
