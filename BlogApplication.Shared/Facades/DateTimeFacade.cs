using System;
using BlogApplication.Shared.Interfaces.Facades;

namespace BlogApplication.Shared.Facades
{
    public class DateTimeFacade : IDateTimeFacade
    {
        public DateTime Now()
        {
            var dateTime = DateTime.Now;
            return dateTime;
        }
    }
}