using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.AnswerViewModels
{
    public class ShowAnswerViewModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public bool Correct { get; set; }
    }
}
