using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Interfaces.Services.Article;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Constants.StringSymbols;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Enums;

namespace BlogApplication.Domain.Services.Article
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class ArticleService : IArticleService
    {
        private readonly IArticleCommentExpressions _articleCommentExpressions;
        private readonly IArticleExpressions _articleExpressions;
        private readonly IModelMapper _modelMapper;
        private readonly IRepository<ArticleCommentEntity> _articleCommentRepository;
        private readonly IRepository<ArticleEntity> _articleRepository;

        public ArticleService(
            IArticleCommentExpressions articleCommentExpressions,
            IArticleExpressions articleExpressions,
            IModelMapper modelMapper,
            IRepository<ArticleCommentEntity> articleCommentRepository,
            IRepository<ArticleEntity> articleRepository)
        {
            _articleCommentExpressions = articleCommentExpressions;
            _articleExpressions = articleExpressions;
            _modelMapper = modelMapper;
            _articleCommentRepository = articleCommentRepository;
            _articleRepository = articleRepository;
        }

        public async Task UpdateArticleAsync(ArticleEntity entity, bool isImageDeleted = false)
        {
            var article = await _articleRepository.FirstOrDefaultAsync(_articleExpressions.ById(entity.Id));
            if (article == null) return;
            article = _modelMapper.Map(entity, article);
            if (isImageDeleted) article.Image = null;
            _articleRepository.Update(article);
        }

        public async Task<IEnumerable<ArticleEntity>> GetLastCommentedArticlesAsync(int count)
        {
            var query = _articleCommentRepository.Query()
                .OrderByDescending(_articleCommentExpressions.ByCreationTime())
                .GroupBy(_articleCommentExpressions.ByArticleId())
                .Select(_articleCommentExpressions.ByFirstOrDefault());

            var comments = await _articleCommentRepository.ToListAsync(query);
            var articles = _modelMapper.Map<ArticleCommentEntity, ArticleEntity>(comments);
            return articles;
        }

        public async Task<IEnumerable<ArticleEntity>> GetTopArticlesAsync(int count)
        {
            var query = _articleRepository.Query()
                .OrderBy(_articleExpressions.ByLikeCount())
                .Take(count);

            var list = await _articleRepository.ToListAsync(query);
            return list;
        }

        public async Task<IEnumerable<ArticleEntity>> SearchArticlesAsync(string title, int count)
        {
            if (string.IsNullOrEmpty(title)) return new List<ArticleEntity>();

            var split = title
                .Trim()
                .ToLowerInvariant()
                .Split(CharSymbols.Space);

            var query = _articleRepository
                .QueryByExpression(_articleExpressions.ByTitle(split))
                .Take(count);

            var list = await _articleRepository.ToListAsync(query);
            return list;
        }

        public async Task<IEnumerable<byte>> GetArticleImageAsync(long id)
        {
            var article = await _articleRepository.FirstOrDefaultAsync(_articleExpressions.ById(id));
            var bytes = article?.Image ?? new byte[0];
            return bytes;
        }
    }
}