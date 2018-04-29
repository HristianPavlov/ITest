using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Infrastructure.Providers
{
    public class DateTimeProvider:IDateTimeProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
