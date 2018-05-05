using ITest.Data.Models;
using ITest.Data.Models.Enums;
using ITest.DTO;
using ITest.Models.QuestionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }
        
        public string Status  { get; set; }

        public string Name { get; set; }

        public List<QuestionEditModel> Questions { get; set; }

        public CategoryDTO Category { get; set; }

        public int CategoryId { get; set; }


        public int TimeInMinutes { get; set; }

    }
}



