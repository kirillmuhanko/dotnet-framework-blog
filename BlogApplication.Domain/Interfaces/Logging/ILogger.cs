using System;

namespace BlogApplication.Domain.Interfaces.Logging
{
    public interface ILogger
    {
        void Log(Exception exception);
    }
}