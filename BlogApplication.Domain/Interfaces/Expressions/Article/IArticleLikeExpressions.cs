using System;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Expressions.Article
{
    public interface IArticleLikeExpressions : IEntityExpressions<ArticleLikeEntity>
    {
        Expression<Func<ArticleLikeEntity, bool>> ByArticleIdAndUserId(long articleId, string userId);

        Expression<Func<ArticleLikeEntity, bool>> ByDislikeCount(long articleId);

        Expression<Func<ArticleLikeEntity, bool>> ByLikeCount(long articleId);
    }
}