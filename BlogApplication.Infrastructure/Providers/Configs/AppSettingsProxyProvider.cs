using System.Configuration;
using BlogApplication.Infrastructure.Interfaces.Providers.Configs;
using BlogApplication.Models.Constants.DataTypes;
using BlogApplication.Models.DomainModels.Configs;

namespace BlogApplication.Infrastructure.Providers.Configs
{
    public class AppSettingsProxyProvider : IAppSettingsProvider
    {
        private SmtpClientConfigModel _smtpClientConfigModel;

        public SmtpClientConfigModel GetSmtpClientConfig()
        {
            if (_smtpClientConfigModel != null) return _smtpClientConfigModel;
            int.TryParse(ConfigurationManager.AppSettings["SmtpClientPort"], out var port);

            _smtpClientConfigModel = new SmtpClientConfigModel
            {
                EnableSsl = ConfigurationManager.AppSettings["SmtpClientEnableSsl"]
                    .ToLowerInvariant() == BooleanDataTypes.True,
                Port = port,
                Address = ConfigurationManager.AppSettings["SmtpClientAddress"],
                Host = ConfigurationManager.AppSettings["SmtpClientHost"],
                Password = ConfigurationManager.AppSettings["SmtpClientPassword"]
            };

            _smtpClientConfigModel.EnsureThatIsNotEmpty();
            return _smtpClientConfigModel;
        }
    }
}