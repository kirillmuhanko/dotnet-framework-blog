using BlogApplication.Models.Components.Controls;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleDeleteViewModel
    {
        public ArticleDeleteViewModel()
        {
            Title = new LabelComponent();
        }

        public LabelComponent Title { get; set; }

        public long Id { get; set; }
    }
}