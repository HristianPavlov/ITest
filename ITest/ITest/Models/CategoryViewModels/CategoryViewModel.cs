using ITest.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
        public string Category { get; set; }
        public  UserTestState CategoryState{ get; set; }
    }
}
