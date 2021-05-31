using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Services.Article;
using BlogApplication.Domain.Interfaces.Services.Common;
using BlogApplication.Domain.Managers.Base;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Enums;

namespace BlogApplication.Domain.Managers.Article
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class ArticleCommentManager : EntityManagerBase<ArticleCommentEntity>, IArticleCommentManager
    {
        private readonly IArticleCommentService _articleCommentService;

        public ArticleCommentManager(
            IDatabaseContext databaseContext,
            IEntityService<ArticleCommentEntity> articleCommentEntityService,
            IArticleCommentService articleCommentService)
            : base(
                databaseContext,
                articleCommentEntityService)
        {
            _articleCommentService = articleCommentService;
        }

        public async Task<ArticleCommentDomainModel> GetCommentAsync(ArticleCommentEntity entity)
        {
            var model = await _articleCommentService.LoadCommentAsync(entity);
            return model;
        }

        public async Task<IEnumerable<ArticleCommentDomainModel>> GetCommentsAsync(
            string userId,
            long articleId,
            int pageNumber,
            int pageSize)
        {
            var enumerable = await _articleCommentService.GetCommentsAsync(userId, articleId, pageNumber, pageSize);
            return enumerable;
        }
    }
}