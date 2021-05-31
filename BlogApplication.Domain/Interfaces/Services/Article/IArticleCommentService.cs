using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Services.Article
{
    public interface IArticleCommentService
    {
        Task<ArticleCommentDomainModel> LoadCommentAsync(ArticleCommentEntity entity);

        Task<IEnumerable<ArticleCommentDomainModel>> GetCommentsAsync(
            string userId,
            long articleId,
            int pageNumber,
            int pageSize);
    }
}