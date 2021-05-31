using System.Collections.Generic;
using BlogApplication.Models.ViewModels.Article;
using BlogApplication.Models.ViewModels.Common;

namespace BlogApplication.Models.ViewModels.Home
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            TopArticleCarousel = new ArticleCarouselViewModel();
            ArticleSearch = new ArticleSearchViewModel();
            LastArticles = new List<ArticleCardTextViewModel>();
            LastCommentedArticles = new List<ArticleCardTextViewModel>();
            Articles = new List<ArticleCardViewModel>();
            Pagination = new PaginationViewModel();
        }

        public ArticleCarouselViewModel TopArticleCarousel { get; set; }

        public ArticleSearchViewModel ArticleSearch { get; set; }

        public IEnumerable<ArticleCardTextViewModel> LastArticles { get; set; }

        public IEnumerable<ArticleCardTextViewModel> LastCommentedArticles { get; set; }

        public IEnumerable<ArticleCardViewModel> Articles { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}