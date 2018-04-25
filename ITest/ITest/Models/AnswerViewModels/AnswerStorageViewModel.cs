using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITest.Models.AnswerViewModels
{
    public class AnswerStorageViewModel
    {
        public AnswerStorageViewModel()
        {
            Values = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Values { get; set; }

        public void Add(string key, string value)
        {
            Values.Add(key, value);
        }

        //public AnswerStorageViewModel Add(string key, string value)
        //{
        //    Values.Add(key, value);
        //    return this;
        //}
    }
}
