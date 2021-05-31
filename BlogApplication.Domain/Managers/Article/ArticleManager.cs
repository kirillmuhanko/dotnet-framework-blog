using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Managers.Article;
using BlogApplication.Domain.Interfaces.Mapping;
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
    public class ArticleManager : EntityManagerBase<ArticleEntity>, IArticleManager
    {
        private readonly IArticleLikeService _articleLikeService;
        private readonly IArticleService _articleService;
        private readonly IDatabaseContext _databaseContext;
        private readonly IEntityService<ArticleEntity> _articleEntityService;
        private readonly IModelMapper _modelMapper;

        public ArticleManager(
            IDatabaseContext databaseContext,
            IEntityService<ArticleEntity> articleEntityService,
            IArticleLikeService articleLikeService,
            IArticleService articleService,
            IModelMapper modelMapper)
            : base(
                databaseContext,
                articleEntityService)
        {
            _databaseContext = databaseContext;
            _articleEntityService = articleEntityService;
            _articleLikeService = articleLikeService;
            _articleService = articleService;
            _modelMapper = modelMapper;
        }

        public async Task UpdateArticleAsync(ArticleEntity entity, bool isImageDeleted = false)
        {
            await _articleService.UpdateArticleAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<ArticleDomainModel> GetArticleAsync(long id, string userId)
        {
            var article = await _articleEntityService.GetByIdAsync(id);
            if (article == null) return null;
            var articleLike = await _articleLikeService.GetLikeAsync(id, userId);
            var model = _modelMapper.Map<ArticleEntity, ArticleDomainModel>(article);

            if (articleLike != null)
            {
                model.IsLiked = articleLike.IsLiked;
                model.IsLikeDeleted = articleLike.IsDeleted;
            }
            else
            {
                model.IsLikeDeleted = true;
            }

            return model;
        }

        public async Task<IEnumerable<ArticleEntity>> GetLastCommentedArticlesAsync(int count)
        {
            var enumerable = await _articleService.GetLastCommentedArticlesAsync(count);
            return enumerable;
        }

        public async Task<IEnumerable<ArticleEntity>> GetTopArticlesAsync(int count)
        {
            var enumerable = await _articleService.GetTopArticlesAsync(count);
            return enumerable;
        }

        public async Task<IEnumerable<ArticleEntity>> SearchArticlesAsync(string title, int count)
        {
            var enumerable = await _articleService.SearchArticlesAsync(title, count);
            return enumerable;
        }

        public async Task<IEnumerable<byte>> GetArticleImageAsync(long id)
        {
            var enumerable = await _articleService.GetArticleImageAsync(id);
            return enumerable;
        }
    }
}