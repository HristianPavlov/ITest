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
        public List<ShowQuestionViewModel> Questions { get; set; }

        public int NumberOfQuestions { get; set; }

        public string UserId { get; set; }

        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int RemainingTime { get; set; }

        public string Category { get; set; }

        public int TimeInMinutes { get; set; }

        public bool Submitted { get; set; }

        //depricated

        public List<int> CorrectOrderOfQuestionId { get; set; }
        public List<string> StorageOfAnswers { get; set; }
    }
}
