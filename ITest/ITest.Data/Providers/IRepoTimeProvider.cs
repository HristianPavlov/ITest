using System;

namespace ITest.Data.Providers
{
    public interface IRepoTimeProvider
    {
         DateTime GetDateTimeNow();
    }
}