namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleAddCommentReportViewModel
    {
        public ArticleAddCommentReportViewModel()
        {
            CommentReport = new ArticleCommentReportViewModel();
            Comment = new ArticleCommentViewModel();
        }

        public ArticleCommentReportViewModel CommentReport { get; set; }

        public ArticleCommentViewModel Comment { get; set; }
    }
}