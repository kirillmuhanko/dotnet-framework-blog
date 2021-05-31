using System.Collections.Generic;
using BlogApplication.Models.DomainModels.Common;

namespace BlogApplication.Models.ViewModels.Manage
{
    public class ManageViewModel
    {
        public ManageViewModel()
        {
            Logins = new List<ResultStatusDomainModel>();
        }

        public bool BrowserRemembered { get; set; }

        public bool HasPassword { get; set; }

        public bool TwoFactor { get; set; }

        public IList<ResultStatusDomainModel> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}