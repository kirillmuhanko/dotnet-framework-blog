using System;

namespace BlogApplication.Shared.Interfaces.Facades
{
    public interface IGuidFacade
    {
        Guid Create(byte[] bytes);

        Guid NewGuid();

        string ToString(Guid guid);
    }
}