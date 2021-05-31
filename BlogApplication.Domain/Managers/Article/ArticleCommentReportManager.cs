using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Services.Article;
using BlogApplication.Domain.Interfaces.Services.Common;
using BlogApplication.Domain.Managers.Base;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Managers.Article
{
    public class ArticleCommentReportManager : EntityManagerBase<ArticleCommentReportEntity>,
        IArticleCommentReportManager
    {
        private readonly IArticleCommentReportService _articleCommentReportService;

        public ArticleCommentReportManager(
            IDatabaseContext databaseContext,
            IEntityService<ArticleCommentReportEntity> articleCommentReportEntityService,
            IArticleCommentReportService articleCommentReportService)
            : base(
                databaseContext,
                articleCommentReportEntityService)
        {
            _articleCommentReportService = articleCommentReportService;
        }

        public async Task<ArticleCommentReportEntity> GetReportedComment(long id, string userId)
        {
            var entity = await _articleCommentReportService.GetReportedComment(id, userId);
            return entity;
        }
    }
}