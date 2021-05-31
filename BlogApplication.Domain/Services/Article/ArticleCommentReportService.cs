using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Domain.Interfaces.Services.Article;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Enums;

namespace BlogApplication.Domain.Services.Article
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class ArticleCommentReportService : IArticleCommentReportService
    {
        private readonly IArticleCommentReportExpressions _articleCommentReportExpressions;
        private readonly IRepository<ArticleCommentReportEntity> _articleCommentReportRepository;

        public ArticleCommentReportService(
            IArticleCommentReportExpressions articleCommentReportExpressions,
            IRepository<ArticleCommentReportEntity> articleCommentReportRepository)
        {
            _articleCommentReportExpressions = articleCommentReportExpressions;
            _articleCommentReportRepository = articleCommentReportRepository;
        }

        public async Task<ArticleCommentReportEntity> GetReportedComment(long commentId, string userId)
        {
            var expression = _articleCommentReportExpressions.ByCommentIdAndUserId(commentId, userId);
            var entity = await _articleCommentReportRepository.FirstOrDefaultAsync(expression);
            return entity;
        }
    }
}