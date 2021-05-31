using System;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Expressions.Article
{
    public interface IArticleCommentReportExpressions : IEntityExpressions<ArticleCommentReportEntity>
    {
        Expression<Func<ArticleCommentReportEntity, bool>> ByCommentIdAndUserId(long commentId, string userId = null);
    }
}