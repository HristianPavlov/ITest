using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.DTO
{
    public class SolveTestDTO
    {
        public ICollection<QuestionDTO> Questions { get; set; }

        public List<int> CorrectOrderOfQuestionId { get; set; }

        public string UserId { get; set; }

        public int Id { get; set; }

        public List<string> StorageOfAnswers { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int RemainingTime { get; set; }

        public string Category { get; set; }

        public int TimeInMinutes { get; set; }

        public bool Submitted { get; set; }
    }
}
