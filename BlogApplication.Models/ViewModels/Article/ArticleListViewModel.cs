using System.Collections.Generic;
using BlogApplication.Models.ViewModels.Common;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleListViewModel
    {
        public ArticleListViewModel()
        {
            Articles = new List<ArticleViewModel>();
            Pagination = new PaginationViewModel();
        }

        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}