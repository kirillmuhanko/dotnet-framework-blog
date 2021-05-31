using System;
using BlogApplication.Shared.Interfaces.Facades;

namespace BlogApplication.Shared.Facades
{
    public class RandomFacade : IRandomFacade
    {
        public Random Create(int seed)
        {
            return new Random(seed);
        }

        public void NextBytes(Random random, byte[] buffer)
        {
            random.NextBytes(buffer);
        }
    }
}