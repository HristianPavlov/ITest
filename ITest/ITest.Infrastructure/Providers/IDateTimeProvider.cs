using System;

namespace ITest.Infrastructure.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTimeNow();
    }
}