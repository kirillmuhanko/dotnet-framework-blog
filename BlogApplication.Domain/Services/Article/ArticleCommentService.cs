using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Domain.Interfaces.Managers.User;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Interfaces.Predicates.DomainModels;
using BlogApplication.Domain.Interfaces.Services.Article;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Enums;
using BlogApplication.Shared.Extensions;

namespace BlogApplication.Domain.Services.Article
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class ArticleCommentService : IArticleCommentService
    {
        private readonly IArticleCommentDomainModelPredicates _articleCommentDomainModelPredicates;
        private readonly IArticleCommentExpressions _articleCommentExpressions;
        private readonly IModelMapper _modelMapper;
        private readonly IRepository<ArticleCommentEntity> _articleCommentRepository;
        private readonly IUserManager _userManager;

        public ArticleCommentService(
            IArticleCommentDomainModelPredicates articleCommentDomainModelPredicates,
            IArticleCommentExpressions articleCommentExpressions,
            IModelMapper modelMapper,
            IRepository<ArticleCommentEntity> articleCommentRepository,
            IUserManager userManager)
        {
            _articleCommentDomainModelPredicates = articleCommentDomainModelPredicates;
            _articleCommentExpressions = articleCommentExpressions;
            _modelMapper = modelMapper;
            _articleCommentRepository = articleCommentRepository;
            _userManager = userManager;
        }

        public async Task<ArticleCommentDomainModel> LoadCommentAsync(ArticleCommentEntity entity)
        {
            var expression = _articleCommentExpressions.ByUser();
            await _articleCommentRepository.LoadReferenceAsync(entity, expression);
            var model = _modelMapper.Map<ArticleCommentEntity, ArticleCommentDomainModel>(entity);
            return model;
        }

        public async Task<IEnumerable<ArticleCommentDomainModel>> GetCommentsAsync(
            string userId,
            long articleId,
            int pageNumber,
            int pageSize)
        {
            var query = _articleCommentRepository
                .QueryByExpression(_articleCommentExpressions.ByArticleId(articleId))
                .OrderByDescending(_articleCommentExpressions.ByCreationTime())
                .Page(pageNumber, pageSize);

            var entities = await _articleCommentRepository.ToListAsync(query);

            var comments = _modelMapper
                .Map<ArticleCommentEntity, ArticleCommentDomainModel>(entities)
                .ToList();

            var isUserSubscriber = await _userManager.IsInRoleAsync(userId, UserRoles.Subscriber);
            if (!isUserSubscriber) return comments;

            var filteredComments = comments
                .Where(_articleCommentDomainModelPredicates.ByUserId(userId))
                .ToList();

            foreach (var model in filteredComments)
                model.IsEditable = true;

            return comments;
        }
    }
}