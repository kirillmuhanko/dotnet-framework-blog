using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BlogApplication.Infrastructure.Interfaces.Providers.Configs;
using Microsoft.AspNet.Identity;

namespace BlogApplication.Infrastructure.Services
{
    public class EmailService : IIdentityMessageService
    {
        private readonly IAppSettingsProvider _appSettingsProvider;

        public EmailService(IAppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            var config = _appSettingsProvider.GetSmtpClientConfig();

            using (var mailMessage = new MailMessage())
            {
                using (var smtpClient = new SmtpClient(config.Host))
                {
                    mailMessage.From = new MailAddress(config.Address);
                    mailMessage.To.Add(message.Destination);
                    mailMessage.Subject = message.Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = message.Body;

                    smtpClient.Port = config.Port;
                    smtpClient.Credentials = new NetworkCredential(config.Address, config.Password);
                    smtpClient.EnableSsl = config.EnableSsl;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}