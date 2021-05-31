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
    public class ArticleLikeService : IArticleLikeService
    {
        private readonly IArticleLikeExpressions _articleLikeExpressions;
        private readonly IRepository<ArticleEntity> _articleRepository;
        private readonly IRepository<ArticleLikeEntity> _articleLikeRepository;

        public ArticleLikeService(
            IArticleLikeExpressions articleLikeExpressions,
            IRepository<ArticleEntity> articleRepository,
            IRepository<ArticleLikeEntity> articleLikeRepository)
        {
            _articleLikeExpressions = articleLikeExpressions;
            _articleRepository = articleRepository;
            _articleLikeRepository = articleLikeRepository;
        }

        public async Task LikeArticleAsync(ArticleEntity article, string userId, bool isLiked, bool isDeleted)
        {
            var like = await GetLikeAsync(article.Id, userId) ?? new ArticleLikeEntity();
            like.ArticleId = article.Id;
            like.IsLiked = isLiked;
            like.IsDeleted = isDeleted;
            _articleLikeRepository.AddOrUpdate(like);
        }

        public async Task UpdateArticleLikeCountAsync(ArticleEntity article)
        {
            var byLikeCountExpression = _articleLikeExpressions.ByLikeCount(article.Id);
            var byDislikeCountExpression = _articleLikeExpressions.ByDislikeCount(article.Id);
            article.LikeCount = await _articleLikeRepository.CountAsync(byLikeCountExpression);
            article.DislikeCount = await _articleLikeRepository.CountAsync(byDislikeCountExpression);
            _articleRepository.Update(article);
        }

        public async Task<ArticleLikeEntity> GetLikeAsync(long articleId, string userId = null)
        {
            var expression = _articleLikeExpressions.ByArticleIdAndUserId(articleId, userId);
            var entity = await _articleLikeRepository.FirstOrDefaultAsync(expression);
            return entity;
        }
    }
}