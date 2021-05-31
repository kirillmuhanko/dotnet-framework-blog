using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Models.ViewModels.Home;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Web.Controllers
{
    [Authorize]
    public class HomeController : WebControllerBase
    {
        private readonly IArticleManager _articleManager;
        private readonly IModelMapper _modelMapper;

        public HomeController(
            IArticleManager articleManager,
            IModelMapper modelMapper)
        {
            _articleManager = articleManager;
            _modelMapper = modelMapper;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index(int page = 1)
        {
            var pagination = await _articleManager.GetPaginationAsync(page, 12);
            var articles = await _articleManager.GetListOrderedByDateTime(pagination);
            var lastArticles = await _articleManager.GetListOrderedByDateTime(5, true);
            var topArticles = await _articleManager.GetTopArticlesAsync(3);
            var lastCommentedArticles = await _articleManager.GetLastCommentedArticlesAsync(3);

            var model = new HomeViewModel();
            model.Articles = _modelMapper.Map(articles, model.Articles);
            model.LastArticles = _modelMapper.Map(lastArticles, model.LastArticles);
            model.TopArticleCarousel.Articles = _modelMapper.Map(topArticles, model.TopArticleCarousel.Articles);
            model.LastCommentedArticles = _modelMapper.Map(lastCommentedArticles, model.LastCommentedArticles);
            model.Pagination = _modelMapper.Map(pagination, model.Pagination);
            model.Pagination.Calculate();

            var viewResult = View(model);
            return viewResult;
        }
    }
}