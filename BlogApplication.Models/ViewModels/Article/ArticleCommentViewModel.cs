namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleCommentViewModel
    {
        public long ArticleId { set; get; }

        public long Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}