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
        public Guid Id { get; set; }


        public int Status { get; set; }

        public string Name { get; set; }

        public List<CreateQuestionViewModel> Questions { get; set; }

       // public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }


        public List<string> CategoryNames { get; set; }


        public int TimeInMinutes { get; set; }
    }
}
