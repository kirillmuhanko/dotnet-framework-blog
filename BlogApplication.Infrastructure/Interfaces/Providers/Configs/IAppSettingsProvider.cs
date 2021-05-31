using BlogApplication.Models.DomainModels.Configs;

namespace BlogApplication.Infrastructure.Interfaces.Providers.Configs
{
    public interface IAppSettingsProvider
    {
        SmtpClientConfigModel GetSmtpClientConfig();
    }
}