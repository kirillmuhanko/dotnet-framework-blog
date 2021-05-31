using BlogApplication.Models.Components.Controls;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleViewModel
    {
        public LabelComponent Text { get; set; }

        public LabelComponent Title { get; set; }

        public long Id { get; set; }
    }
}