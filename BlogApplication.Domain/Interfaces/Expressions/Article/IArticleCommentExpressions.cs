using System;
using System.Linq;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Expressions.Article
{
    public interface IArticleCommentExpressions : IEntityExpressions<ArticleCommentEntity>
    {
        Expression<Func<ArticleCommentEntity, bool>> ByArticleId(long articleId);

        Expression<Func<ArticleCommentEntity, long>> ByArticleId();

        Expression<Func<IGrouping<long, ArticleCommentEntity>, ArticleCommentEntity>> ByFirstOrDefault();
    }
}