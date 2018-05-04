using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITest.Data.Models
{
    public class Answer : DataModel
    {

        public ICollection<UserTestAnswers> UserTests { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool Correct { get; set; }

        [Required]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}