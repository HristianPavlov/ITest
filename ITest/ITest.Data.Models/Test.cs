﻿using ITest.Data.Models.Abstracts;
using ITest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITest.Data.Models
{
  public  class Test:DataModel
    {

        //public Test()
        //{
        //    this.Questions = new HashSet<Question>();
        //}



        //[ForeignKey("User")]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public TestStatus Status { get; set; }
        
        public ICollection<Question> Questions { get; set; }

        public ICollection<UserTests> Users { get; set; }



    }
}
