using System;
using System.Linq;
using System.Linq.Expressions;
using BlogApplication.Domain.Expressions.Common;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Expressions.Article
{
    public class ArticleCommentExpressions : EntityExpressions<ArticleCommentEntity>, IArticleCommentExpressions
    {
        public Expression<Func<ArticleCommentEntity, bool>> ByArticleId(long articleId)
        {
            return t => t.ArticleId.Equals(articleId);
        }

        public Expression<Func<ArticleCommentEntity, long>> ByArticleId()
        {
            return t => t.ArticleId;
        }

        public Expression<Func<IGrouping<long, ArticleCommentEntity>, ArticleCommentEntity>> ByFirstOrDefault()
        {
            return t => t.FirstOrDefault();
        }
    }
}