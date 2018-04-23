using ITest.Models.AnswerViewModels;
using ITest.Models.QuestionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.TestViewModels
{
    public class SolveTestViewModel
    {
        public ICollection<ShowQuestionViewModel> Questions { get; set; }

        //public AnswerStorageViewModel Storage { get; set; }

        public int Id { get; set; }

        public List<string> StorageOfAnswers { get; set; }
    }
}
