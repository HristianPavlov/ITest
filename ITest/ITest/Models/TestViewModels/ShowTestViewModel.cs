using ITest.Models.QuestionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.TestViewModels
{
    public class ShowTestViewModel
    {
        public ICollection<ShowQuestionViewModel> Questions { get; set; }
    }
}
