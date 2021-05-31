using System;
using System.Linq.Expressions;
using BlogApplication.Domain.Expressions.Common;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Expressions.Article
{
    public class ArticleLikeExpressions : EntityExpressions<ArticleLikeEntity>, IArticleLikeExpressions
    {
        public Expression<Func<ArticleLikeEntity, bool>> ByArticleIdAndUserId(long articleId, string userId)
        {
            return t => t.ArticleId.Equals(articleId) && t.AddedByUserId.Equals(userId);
        }

        public Expression<Func<ArticleLikeEntity, bool>> ByDislikeCount(long articleId)
        {
            return t => !t.IsDeleted && t.ArticleId.Equals(articleId) && !t.IsLiked;
        }

        public Expression<Func<ArticleLikeEntity, bool>> ByLikeCount(long articleId)
        {
            return t => !t.IsDeleted && t.ArticleId.Equals(articleId) && t.IsLiked;
        }
    }
}