using System.Threading.Tasks;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Services.Article
{
    public interface IArticleCommentReportService
    {
        Task<ArticleCommentReportEntity> GetReportedComment(long id, string userId);
    }
}