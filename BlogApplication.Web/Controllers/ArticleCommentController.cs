using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Interfaces.Providers;
using BlogApplication.Models.Attributes.Authorization;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.ViewModels.Article;
using BlogApplication.Models.ViewModels.Common;

namespace BlogApplication.Web.Controllers
{
    [Authorize]
    public class ArticleCommentController : WebControllerBase
    {
        private readonly IArticleCommentManager _articleCommentManager;
        private readonly IArticleCommentReportManager _articleCommentReportManager;
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IModelMapper _modelMapper;

        public ArticleCommentController(
            IArticleCommentManager articleCommentManager,
            IArticleCommentReportManager articleCommentReportManager,
            IHttpContextProvider httpContextProvider,
            IModelMapper modelMapper)
        {
            _articleCommentManager = articleCommentManager;
            _articleCommentReportManager = articleCommentReportManager;
            _httpContextProvider = httpContextProvider;
            _modelMapper = modelMapper;
        }

        public async Task<ActionResult> Report(long id = 0)
        {
            var model = new ArticleAddCommentReportViewModel();
            var comment = await _articleCommentManager.GetByIdAsync(id);
            var report = await _articleCommentReportManager.GetReportedComment(id, _httpContextProvider.UserId);

            if (comment == null)
                return View(
                    ActionNames.PageResult,
                    new PageResultViewModel(PageResultMessages.NoRecordsFound));

            if (report != null)
                return View(
                    ActionNames.PageResult,
                    new PageResultViewModel(PageResultMessages.YouHaveAlreadyReportedThisComment));

            model.Comment = _modelMapper.Map(comment, model.Comment);
            model.CommentReport.CommentId = id;

            var viewResult = View(model);
            return viewResult;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Subscriber)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Report(ArticleAddCommentReportViewModel model)
        {
            var viewResult = View(model);

            if (ModelHasErrors) return viewResult;

            var comment = await _articleCommentManager.GetByIdAsync(model.Comment.Id);
            model.Comment = _modelMapper.Map<ArticleCommentEntity, ArticleCommentViewModel>(comment);

            var report =
                _modelMapper.Map<ArticleCommentReportViewModel, ArticleCommentReportEntity>(model.CommentReport);

            await _articleCommentReportManager.AddAsync(report);

            var redirectToRouteResult = RedirectToAction(
                ActionNames.Index,
                ControllerNames.Article,
                new {id = model.Comment.ArticleId});

            return redirectToRouteResult;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Subscriber)]
        [ValidateAntiForgeryToken]
        public async Task<int> Delete(long id)
        {
            var result = await _articleCommentManager.DeleteAsync(id, _httpContextProvider.UserId);
            return result;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Subscriber)]
        [ValidateAntiForgeryToken]
        public async Task<int> Update(long id, string text)
        {
            var comment = await _articleCommentManager.GetByIdAsync(id);

            if (comment == null) return 0;

            comment.Text = text;
            var result = await _articleCommentManager.UpdateAsync(comment, _httpContextProvider.UserId);
            return result;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Subscriber)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Add(long id, string text)
        {
            var entity = new ArticleCommentEntity
            {
                ArticleId = id,
                Text = text
            };

            await _articleCommentManager.AddAsync(entity);
            var comment = await _articleCommentManager.GetCommentAsync(entity);
            return Json(comment, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> List(long id, int page = 1, int pageSize = 1)
        {
            var userId = _httpContextProvider.UserId;
            var comments = await _articleCommentManager.GetCommentsAsync(userId, id, page, pageSize);
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}