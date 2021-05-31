using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogApplication.Models.Entities.Base;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.Article
{
    [NoReorder]
    [Table("Articles")]
    public class ArticleEntity : EntityBase
    {
        [Required] public string Title { get; set; }

        public byte[] Image { get; set; }

        public string Text { get; set; }

        public long LikeCount { get; set; }

        public long DislikeCount { get; set; }
    }
}