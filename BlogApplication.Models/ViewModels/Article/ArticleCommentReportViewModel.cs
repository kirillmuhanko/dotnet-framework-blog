using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleCommentReportViewModel
    {
        public ArticleCommentReportViewModel()
        {
            Text = new TextAreaFormGroupComponent();
        }

        public long CommentId { get; set; }

        public long Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Complaint message")]
        [RequiredFormGroup]
        public TextAreaFormGroupComponent Text { get; set; }
    }
}