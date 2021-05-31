using System;
using BlogApplication.Models.DomainModels.Article;

namespace BlogApplication.Domain.Interfaces.Predicates.DomainModels
{
    public interface IArticleCommentDomainModelPredicates
    {
        Func<ArticleCommentDomainModel, bool> ByUserId(string userId);
    }
}