using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.CategoryViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }

    }
}
