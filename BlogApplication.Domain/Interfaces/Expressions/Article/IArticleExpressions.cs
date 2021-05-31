using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Expressions.Article
{
    public interface IArticleExpressions : IEntityExpressions<ArticleEntity>
    {
        Expression<Func<ArticleEntity, bool>> ByTitle(IEnumerable<string> words);

        Expression<Func<ArticleEntity, long>> ByLikeCount();
    }
}