using System.ComponentModel.DataAnnotations.Schema;
using BlogApplication.Models.Entities.Base;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.Article
{
    [NoReorder]
    [Table("ArticleLikes")]
    public class ArticleLikeEntity : EntityBase
    {
        public long ArticleId { set; get; }

        public bool IsLiked { set; get; }

        public bool IsDeleted { set; get; }

        public virtual ArticleEntity Article { get; set; }
    }
}