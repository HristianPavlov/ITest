using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
  public  class QuestionEditDTO
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string TestId { get; set; }

        public bool IsDeleted { get; set; }
        
        public IList<AnswerEditDTO> Answers { get; set; }
    }
}
