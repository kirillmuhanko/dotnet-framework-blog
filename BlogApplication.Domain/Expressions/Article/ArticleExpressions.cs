using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlogApplication.Domain.Expressions.Common;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Expressions.Article
{
    public class ArticleExpressions : EntityExpressions<ArticleEntity>, IArticleExpressions
    {
        public Expression<Func<ArticleEntity, bool>> ByTitle(IEnumerable<string> words)
        {
            return t => words.All(s => t.Title.ToLower().Contains(s));
        }

        public Expression<Func<ArticleEntity, long>> ByLikeCount()
        {
            return t => t.LikeCount;
        }
    }
}