using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
  public  class AnswerEditDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Correct { get; set; }
        public int QuestionId { get; set; }
        
    }
}
