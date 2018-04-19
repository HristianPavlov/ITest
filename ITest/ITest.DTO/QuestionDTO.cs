using System.Collections.Generic;

namespace ITest.DTO
{
    public class QuestionDTO
    {

        public int Id { get; set; }

        public string Content { get; set; }
        
        public int TestId { get; set; }

        public TestDTO Test { get; set; }

        public ICollection<AnswerDTO> Answers { get; set; }



    }
}