using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Services.Article;
using BlogApplication.Domain.Interfaces.Services.Common;
using BlogApplication.Domain.Managers.Base;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Enums;

namespace BlogApplication.Domain.Managers.Article
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class ArticleLikeManager : EntityManagerBase<ArticleLikeEntity>, IArticleLikeManager
    {
        private readonly IArticleLikeService _articleLikeService;
        private readonly IDatabaseContext _databaseContext;
        private readonly IEntityService<ArticleEntity> _articleEntityService;

        public ArticleLikeManager(
            IDatabaseContext databaseContext,
            IEntityService<ArticleLikeEntity> articleLikeEntityService,
            IArticleLikeService articleLikeService,
            IEntityService<ArticleEntity> articleEntityService)
            : base(
                databaseContext,
                articleLikeEntityService)
        {
            _databaseContext = databaseContext;
            _articleLikeService = articleLikeService;
            _articleEntityService = articleEntityService;
        }

        public async Task<int> LikeAsync(long articleId, string userId, bool isLiked, bool isDeleted)
        {
            var article = await _articleEntityService.GetByIdAsync(articleId);
            await _articleLikeService.LikeArticleAsync(article, userId, isLiked, isDeleted);
            var result = await _databaseContext.SaveChangesAsync();
            if (result == 0) return 0;
            await _articleLikeService.UpdateArticleLikeCountAsync(article);
            result = await _databaseContext.SaveChangesAsync();
            return result;
        }
    }
}