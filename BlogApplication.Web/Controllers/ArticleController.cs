using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Interfaces.Providers;
using BlogApplication.Models.Attributes.Authorization;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.ViewModels.Article;
using BlogApplication.Models.ViewModels.Common;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Web.Controllers
{
    [Authorize]
    public class ArticleController : WebControllerBase
    {
        private readonly IArticleLikeManager _articleLikeManager;
        private readonly IArticleManager _articleManager;
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IModelMapper _modelMapper;

        public ArticleController(
            IArticleLikeManager articleLikeManager,
            IArticleManager articleManager,
            IHttpContextProvider httpContextProvider,
            IModelMapper modelMapper)
        {
            _articleLikeManager = articleLikeManager;
            _articleManager = articleManager;
            _httpContextProvider = httpContextProvider;
            _modelMapper = modelMapper;
        }

        [RoleBasedAuthorization(UserRoles.Editor)]
        public ActionResult Add()
        {
            var model = new ArticleAddViewModel();
            var viewResult = View(model);
            return viewResult;
        }

        [RoleBasedAuthorization(UserRoles.Editor)]
        public async Task<ActionResult> Delete(long id = 0)
        {
            var article = await _articleManager.GetByIdAsync(id);

            if (article == null)
                return View(
                    ActionNames.PageResult,
                    new PageResultViewModel(PageResultMessages.NoRecordsFound));

            var model = _modelMapper.Map<ArticleEntity, ArticleDeleteViewModel>(article);
            var viewResult = View(model);
            return viewResult;
        }

        [RoleBasedAuthorization(UserRoles.Editor)]
        public async Task<ActionResult> Edit(long id = 0)
        {
            var article = await _articleManager.GetByIdAsync(id);
            var model = _modelMapper.Map<ArticleEntity, ArticleEditViewModel>(article);
            var viewResult = View(model);
            return viewResult;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index(long id = 0)
        {
            var article = await _articleManager.GetArticleAsync(id, _httpContextProvider.UserId);

            if (article == null)
                return View(
                    ActionNames.PageResult,
                    new PageResultViewModel(PageResultMessages.PageNotFound));

            var model = _modelMapper.Map<ArticleDomainModel, ArticleIndexViewModel>(article);
            var viewResult = View(model);
            return viewResult;
        }

        [RoleBasedAuthorization(UserRoles.Editor)]
        public async Task<ActionResult> List(int page = 1)
        {
            var pagination = await _articleManager.GetPaginationAsync(page, 10, _httpContextProvider.UserId);
            var articles = await _articleManager.GetListOrderedByDateTime(pagination, _httpContextProvider.UserId);

            var model = new ArticleListViewModel();
            model.Articles = _modelMapper.Map(articles, model.Articles);
            model.Pagination = _modelMapper.Map(pagination, model.Pagination);
            model.Pagination.Calculate();

            var viewResult = View(model);
            return viewResult;
        }

        [AllowAnonymous]
        [OutputCache(CacheProfile = CacheProfiles.Cache1Hour)]
        public async Task<FileContentResult> GetCachedImage(long id = 0)
        {
            var fileContentResult = await GetImage(id);
            return fileContentResult;
        }

        [AllowAnonymous]
        public async Task<FileContentResult> GetImage(long id = 0)
        {
            var bytes = await _articleManager.GetArticleImageAsync(id);
            var fileContentResult = File(bytes.ToArray(), ContentTypes.ImageJpg);
            return fileContentResult;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Editor)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ArticleAddViewModel model)
        {
            if (ModelHasErrors)
            {
                var viewResult = View(model);
                return viewResult;
            }

            var article = _modelMapper.Map<ArticleAddViewModel, ArticleEntity>(model);
            await _articleManager.AddAsync(article);
            var redirectToRouteResult = RedirectToAction(ActionNames.List, ControllerNames.Article);
            return redirectToRouteResult;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Editor)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ArticleDeleteViewModel model)
        {
            if (ModelHasErrors)
            {
                var viewResult = View(model);
                return viewResult;
            }

            await _articleManager.DeleteAsync(model.Id);
            var redirectToRouteResult = RedirectToAction(ActionNames.List, ControllerNames.Article);
            return redirectToRouteResult;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Editor)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ArticleEditViewModel model)
        {
            if (ModelHasErrors)
            {
                var viewResult = View(model);
                return viewResult;
            }

            var article = _modelMapper.Map<ArticleEditViewModel, ArticleEntity>(model);
            await _articleManager.UpdateArticleAsync(article, model.IsImageDeleted.IsChecked);
            var redirectToRouteResult = RedirectToAction(ActionNames.List, ControllerNames.Article);
            return redirectToRouteResult;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(ArticleSearchViewModel model)
        {
            var viewModel = new ArticleCardListViewModel();
            var viewResult = View(viewModel);

            if (ModelHasErrors)
                return viewResult;

            var article = await _articleManager.SearchArticlesAsync(model.Query.Text, 16);
            viewModel.Articles = _modelMapper.Map<ArticleEntity, ArticleCardViewModel>(article);
            return viewResult;
        }

        [HttpPost]
        [RoleBasedAuthorization(UserRoles.Rater)]
        [ValidateAntiForgeryToken]
        public async Task<int> Like(long articleId, bool isLiked, bool isDeleted)
        {
            var result = 0;

            if (ModelHasErrors)
                return result;

            result = await _articleLikeManager.LikeAsync(articleId, _httpContextProvider.UserId, isLiked, isDeleted);
            return result;
        }
    }
}