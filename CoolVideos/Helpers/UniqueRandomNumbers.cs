using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolVideos.Helpers
{
    public class UniqueRandomNumbers
    {
        public static HashSet<int> Generate(int maxListLength, int minRandomValue, int maxRandomValue)
        {
            Random random = new Random();
            HashSet<int> outputSet = new HashSet<int>();

            for (int i = 0; i < maxListLength; i++)
            {
                int number = random.Next(minRandomValue, maxRandomValue + 1);

                if (!outputSet.Contains(number))
                {
                    outputSet.Add(number);
                }
            }

            return outputSet;
        }
    }
}
