using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Managers.Base;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Managers.Article
{
    public interface IArticleCommentReportManager : IEntityManagerBase<ArticleCommentReportEntity>
    {
        Task<ArticleCommentReportEntity> GetReportedComment(long id, string userId);
    }
}