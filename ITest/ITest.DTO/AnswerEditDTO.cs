using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
  public  class AnswerEditDTO
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool Correct { get; set; }
        public string QuestionId { get; set; }
        
    }
}
