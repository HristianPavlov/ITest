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
              
        public TestStatusDTO Status { get; set; }

        public string Content;

        public List<CreateQuestionViewModel> Questions { get; set; }
    }
}
