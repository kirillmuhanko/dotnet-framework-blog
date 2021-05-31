using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Services.Article
{
    public interface IArticleService
    {
        Task UpdateArticleAsync(ArticleEntity entity, bool isImageDeleted = false);

        Task<IEnumerable<ArticleEntity>> GetLastCommentedArticlesAsync(int count);

        Task<IEnumerable<ArticleEntity>> GetTopArticlesAsync(int count);

        Task<IEnumerable<ArticleEntity>> SearchArticlesAsync(string title, int count);

        Task<IEnumerable<byte>> GetArticleImageAsync(long id);
    }
}