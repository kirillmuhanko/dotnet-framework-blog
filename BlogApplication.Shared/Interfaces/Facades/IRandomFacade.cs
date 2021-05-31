using System;

namespace BlogApplication.Shared.Interfaces.Facades
{
    public interface IRandomFacade
    {
        Random Create(int seed);

        void NextBytes(Random random, byte[] buffer);
    }
}