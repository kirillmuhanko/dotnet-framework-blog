using System;
using BlogApplication.Shared.Interfaces.Facades;

namespace BlogApplication.Shared.Facades
{
    public class GuidFacade : IGuidFacade
    {
        public Guid Create(byte[] bytes)
        {
            var guid = new Guid(bytes);
            return guid;
        }

        public Guid NewGuid()
        {
            var guid = Guid.NewGuid();
            return guid;
        }

        public string ToString(Guid guid)
        {
            var str = guid.ToString();
            return str;
        }
    }
}