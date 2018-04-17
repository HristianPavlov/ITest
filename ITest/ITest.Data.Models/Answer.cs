using ITest.Data.Models.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace ITest.Data.Models
{
    public class Answer:DataModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public bool Correct { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

    }
}