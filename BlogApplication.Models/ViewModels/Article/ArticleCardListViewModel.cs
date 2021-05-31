using System.Collections.Generic;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleCardListViewModel
    {
        public ArticleCardListViewModel()
        {
            Articles = new List<ArticleCardViewModel>();
        }

        public IEnumerable<ArticleCardViewModel> Articles { get; set; }
    }
}