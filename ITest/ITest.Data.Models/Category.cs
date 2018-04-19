using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITest.Data.Models
{
    public class Category : DataModel
    {
        [Required]
        public string Name { get; set; }
    }
}
