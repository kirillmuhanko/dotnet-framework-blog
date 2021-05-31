using System.Collections.Generic;
using BlogApplication.Models.ViewModels.Common;
using BlogApplication.Models.ViewModels.User;

namespace BlogApplication.Models.ViewModels.Admin
{
    public class AdminUserListViewModel
    {
        public AdminUserListViewModel()
        {
            Users = new List<UserViewModel>();
            Pagination = new PaginationViewModel();
        }

        public IEnumerable<UserViewModel> Users { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}