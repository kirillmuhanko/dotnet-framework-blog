using System;
using System.Linq.Expressions;
using BlogApplication.Domain.Expressions.Common;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Expressions.Article
{
    public class ArticleCommentReportExpressions : EntityExpressions<ArticleCommentReportEntity>,
        IArticleCommentReportExpressions
    {
        public Expression<Func<ArticleCommentReportEntity, bool>> ByCommentIdAndUserId(long commentId, string userId)
        {
            return t => t.CommentId.Equals(commentId) && t.AddedByUserId.Equals(userId);
        }
    }
}