using ITest.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.AnswerViewModels
{
    public class CreatingAnswerViewModel
    {

        
        // [Required]
        [MinLength(5)]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Required]
        public bool Correct { get; set; }

        // public int QuestionId { get; set; }

        //public QuestionDTO Question { get; set; }

    }
}
