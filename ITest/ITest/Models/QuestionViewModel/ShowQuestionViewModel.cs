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

        public Guid Id { get; set; }

        public Guid SelectedAnswerId { get; set; }

        public IList<ShowAnswerViewModel> Answers { get; set; }
    }
}
