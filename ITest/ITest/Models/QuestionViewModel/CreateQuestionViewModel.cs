using ITest.Models.AnswerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.QuestionViewModel
{
    public class CreateQuestionViewModel
    {
        [MinLength(5)]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public bool isRemoved { get; set;}


        public List<CreateAnswerViewModel> Answers { get; set; }
        //public ICollection<CreateAnswerViewModel> Answers { get; set; }

    }
}
