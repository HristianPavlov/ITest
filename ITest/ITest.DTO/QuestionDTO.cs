using System;
using System.Collections.Generic;

namespace ITest.DTO
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
        
        public Guid TestId { get; set; }

        public TestDTO Test { get; set; }

        public Guid SelectedAnswerId { get; set; }

        public ICollection<AnswerDTO> Answers { get; set; }
    }
}