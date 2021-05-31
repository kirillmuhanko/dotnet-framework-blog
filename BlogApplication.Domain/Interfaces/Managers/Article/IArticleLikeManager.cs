using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Managers.Base;
using BlogApplication.Models.Entities.Article;

namespace BlogApplication.Domain.Interfaces.Managers.Article
{
    public interface IArticleLikeManager : IEntityManagerBase<ArticleLikeEntity>
    {
        Task<int> LikeAsync(long articleId, string userId, bool isLiked, bool isDeleted);
    }
}