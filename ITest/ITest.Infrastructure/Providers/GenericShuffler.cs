using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITest.Infrastructure.Providers
{
    public class GenericShuffler : IGenericShuffler
    {
        private readonly IRandomProvider rand;

        public GenericShuffler(IRandomProvider rand)
        {
            this.rand = rand;
        }
        public ICollection<T> Shuffle<T>(List<T> list)
        {
            var listCount = list.Count();

            for (int i = 0; i < list.Count; i++)
            {
                var randomIndex = i + rand.GiveMeRandomNumber(listCount - i);
                var temp = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = temp;
            }
            return list;
        }
    }
}
