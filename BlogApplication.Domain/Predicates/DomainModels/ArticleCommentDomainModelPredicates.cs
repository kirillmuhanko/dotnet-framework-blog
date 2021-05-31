using System;
using BlogApplication.Domain.Interfaces.Predicates.DomainModels;
using BlogApplication.Models.DomainModels.Article;

namespace BlogApplication.Domain.Predicates.DomainModels
{
    public class ArticleCommentDomainModelPredicates : IArticleCommentDomainModelPredicates
    {
        public Func<ArticleCommentDomainModel, bool> ByUserId(string userId)
        {
            return t => t.UserId == userId;
        }
    }
}