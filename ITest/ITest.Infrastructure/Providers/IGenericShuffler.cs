using System.Collections.Generic;

namespace ITest.Infrastructure.Providers
{
    public interface IGenericShuffler
    {
        ICollection<T> Shuffle<T>(List<T> list);
    }
}