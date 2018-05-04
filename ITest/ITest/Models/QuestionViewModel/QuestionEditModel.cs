using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.QuestionViewModel
{
    public class QuestionEditModel
    {
        //[MinLength(5)]
        //[MaxLength(500)]
        //[DataType(DataType.Text)]
        public int Id { get; set; }

        public string Content { get; set; }

        public int TestId { get; set; }

        public List<AnswerEditModel> Answers { get; set; }
                      
        
    }
}
