using ITest.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITest.Data.Models
{
    public class Question : DataModel
    {

        //public Question()
        //{
        //    this.Answers = new HashSet<Answer>();

        //}


        [Required]
        public string Content { get; set; }

        
        public int TestId { get; set; }

        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }



    }
}
