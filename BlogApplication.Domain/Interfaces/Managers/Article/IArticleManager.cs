using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Managers.Base;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Managers.Article
{
    public interface IArticleManager : IEntityManagerBase<ArticleEntity>
    {
        Task UpdateArticleAsync(ArticleEntity entity, bool isImageDeleted = false);

        Task<ArticleDomainModel> GetArticleAsync(long id, string userId);

        Task<IEnumerable<ArticleEntity>> GetLastCommentedArticlesAsync(int count);

        Task<IEnumerable<ArticleEntity>> GetTopArticlesAsync(int count);

        Task<IEnumerable<ArticleEntity>> SearchArticlesAsync(string title, int count);

        Task<IEnumerable<byte>> GetArticleImageAsync(long id);
    }
}