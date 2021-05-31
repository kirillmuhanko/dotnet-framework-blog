using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Managers.Base;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Managers.Article
{
    public interface IArticleCommentManager : IEntityManagerBase<ArticleCommentEntity>
    {
        Task<ArticleCommentDomainModel> GetCommentAsync(ArticleCommentEntity entity);

        Task<IEnumerable<ArticleCommentDomainModel>> GetCommentsAsync(
            string userId,
            long articleId,
            int pageNumber,
            int pageSize);
    }
}