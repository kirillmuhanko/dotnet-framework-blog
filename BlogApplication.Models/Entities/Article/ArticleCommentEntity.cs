using System.ComponentModel.DataAnnotations.Schema;
using BlogApplication.Models.Entities.Base;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.Article
{
    [NoReorder]
    [Table("ArticleComments")]
    public class ArticleCommentEntity : EntityBase
    {
        public long ArticleId { set; get; }

        public string Text { get; set; }

        public virtual ArticleEntity Article { get; set; }
    }
}