using BlogApplication.Models.ViewModels.Article;

namespace BlogApplication.Models.ViewModels.Admin
{
    public class AdminEditReportedCommentViewModel
    {
        public AdminEditReportedCommentViewModel()
        {
            CommentReport = new ArticleCommentReportViewModel();
            Comment = new ArticleCommentViewModel();
        }

        public ArticleCommentReportViewModel CommentReport { get; set; }

        public ArticleCommentViewModel Comment { get; set; }
    }
}