using System.Collections.Generic;

namespace BlogApplication.Models.ViewModels.Admin
{
    public class AdminEditUserViewModel
    {
        public AdminEditUserViewModel()
        {
            Roles = new List<AdminEditRoleViewModel>();
        }

        public IList<AdminEditRoleViewModel> Roles { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}