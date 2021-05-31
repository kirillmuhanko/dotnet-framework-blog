using System;
using BlogApplication.Domain.Interfaces.Logging;

namespace BlogApplication.Domain.Logging
{
    public class Logger : ILogger
    {
        public void Log(Exception exception)
        {
        }
    }
}