using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BlogApplication.Infrastructure.Services
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var task = Task.FromResult(0);
            return task;
        }
    }
}