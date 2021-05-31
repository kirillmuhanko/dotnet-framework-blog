using System.Threading.Tasks;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Services.Article
{
    public interface IArticleLikeService
    {
        Task LikeArticleAsync(ArticleEntity article, string userId, bool isLiked, bool isDeleted);

        Task UpdateArticleLikeCountAsync(ArticleEntity article);

        Task<ArticleLikeEntity> GetLikeAsync(long articleId, string userId);
    }
}