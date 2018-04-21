using ITest.Models.AnswerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.QuestionViewModel
{
    public class ShowQuestionViewModel
    {
        public string Content { get; set; }

        public ICollection<ShowAnswerViewModel> Answers { get; set; }
    }
}
