using ITest.DTO;
using ITest.Models.QuestionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models
{
    public class CreateTestViewModel
    {
        public int Id { get; set; }

        

        public int Status { get; set; }

        public string Name { get; set; }

        public List<CreateQuestionViewModel> Questions { get; set; }

        public int CategoryId { get; set; }

        

        public int TimeInMinutes { get; set; }

       

    }
}
