using System.Collections.Generic;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleCarouselViewModel
    {
        public ArticleCarouselViewModel()
        {
            Articles = new List<ArticleCardViewModel>();
        }

        public IList<ArticleCardViewModel> Articles { get; set; }

        public long Id { get; set; }
    }
}