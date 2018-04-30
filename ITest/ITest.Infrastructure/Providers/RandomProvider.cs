using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Infrastructure.Providers
{
    public class RandomProvider : IRandomProvider
    {
        //Gives random number smaller or equal to
        public int GiveMeRandomNumber(int number)
        {
            var random = new Random();
            return random.Next(number);
        }
    }
}
