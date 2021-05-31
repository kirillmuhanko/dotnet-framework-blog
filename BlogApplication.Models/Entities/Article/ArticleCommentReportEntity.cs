using System.ComponentModel.DataAnnotations.Schema;
using BlogApplication.Models.Entities.Base;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.Article
{
    [NoReorder]
    [Table("ArticleCommentReports")]
    public class ArticleCommentReportEntity : EntityBase
    {
        public long CommentId { set; get; }

        public string Text { get; set; }

        public virtual ArticleCommentEntity Comment { get; set; }
    }
}