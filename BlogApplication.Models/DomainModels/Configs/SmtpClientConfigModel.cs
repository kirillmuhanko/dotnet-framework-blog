using System;

namespace BlogApplication.Models.DomainModels.Configs
{
    public class SmtpClientConfigModel
    {
        public bool EnableSsl { get; set; }

        public int Port { get; set; }

        public string Address { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public void EnsureThatIsNotEmpty()
        {
            if (string.IsNullOrEmpty(Address) ||
                string.IsNullOrEmpty(Host) ||
                string.IsNullOrEmpty(Password))
                throw new ArgumentException($"{nameof(SmtpClientConfigModel)} is empty.");
        }
    }
}