using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Data.Providers
{
    public class RepoTimeProvider: IRepoTimeProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
