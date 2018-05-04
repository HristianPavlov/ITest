using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
  public  class QuestionEditDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int TestId { get; set; }

     
        public IList<AnswerEditDTO> Answers { get; set; }
    }
}
