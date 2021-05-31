using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Models.Attributes.Authorization;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.ViewModels.Admin;
using BlogApplication.Models.ViewModels.Common;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Web.Controllers
{
    [RoleBasedAuthorization(UserRoles.Administrator)]
    public class AdminController : WebControllerBase
    {
        private readonly IArticleCommentManager _articleCommentManager;
        private readonly IArticleCommentReportManager _articleCommentReportManager;
        private readonly IModelMapper _modelMapper;
        private readonly IUserManager _userManager;

        public AdminController(
            IArticleCommentManager articleCommentManager,
            IArticleCommentReportManager articleCommentReportManager,
            IModelMapper modelMapper,
            IUserManager userManager)
        {
            _articleCommentManager = articleCommentManager;
            _articleCommentReportManager = articleCommentReportManager;
            _modelMapper = modelMapper;
            _userManager = userManager;
        }

        public async Task<ActionResult> AllowArticleComment(AdminEditReportedCommentViewModel model)
        {
            await _articleCommentReportManager.DeleteAsync(model.CommentReport.Id);

            var redirectToRouteResult =
                RedirectToAction(ActionNames.ReportedArticleComment, ControllerNames.Admin);

            return redirectToRouteResult;
        }

        public async Task<ActionResult> BlockArticleComment(AdminEditReportedCommentViewModel model)
        {
            await _articleCommentManager.DeleteAsync(model.Comment.Id);
            await _articleCommentManager.DeleteAsync(model.CommentReport.Id);

            var redirectToRouteResult =
                RedirectToAction(ActionNames.ReportedArticleComment, ControllerNames.Admin);

            return redirectToRouteResult;
        }

        public async Task<ActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return View(
                    ActionNames.PageResult,
                    new PageResultViewModel(PageResultMessages.NoRecordsFound));

            var model = new AdminEditUserViewModel();
            var roles = await _userManager.GetRolesAsync(id);
            model = _modelMapper.Map(user, model);
            model.Roles = _modelMapper.Map(roles, model.Roles);

            var viewResult = View(model);
            return viewResult;
        }

        public async Task<ActionResult> ReportedArticleComment()
        {
            var model = new AdminEditReportedCommentViewModel();
            var report = await _articleCommentReportManager.GetFirstOrDefaultAsync();

            if (report == null)
                return View(
                    ActionNames.PageResult,
                    new PageResultViewModel(PageResultMessages.NoRecordsFound));

            _modelMapper.Map(report, model.CommentReport);
            _modelMapper.Map(report.Comment, model.Comment);

            var viewResult = View(model);
            return viewResult;
        }

        public async Task<ActionResult> UserList(int page = 1)
        {
            var pagination = await _userManager.GetPaginationAsync(page, 10);
            var users = await _userManager.GetUsersAsync(pagination);

            var model = new AdminUserListViewModel();
            model.Users = _modelMapper.Map(users, model.Users);
            model.Pagination = _modelMapper.Map(pagination, model.Pagination);
            model.Pagination.Calculate();

            var viewResult = View(model);
            return viewResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(AdminEditUserViewModel model)
        {
            var roles = _modelMapper.Map<AdminEditRoleViewModel, UserRoleDomainModel>(model.Roles);
            await _userManager.UpdateRoles(model.Id, roles);

            var redirectToRouteResult =
                RedirectToAction(ActionNames.UserList, ControllerNames.Admin);

            return redirectToRouteResult;
        }
    }
}