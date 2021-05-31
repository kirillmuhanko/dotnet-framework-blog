using System;
using BlogApplication.Models.Components.Controls;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleIndexViewModel
    {
        public ArticleIndexViewModel()
        {
            Text = new LabelComponent();
            Title = new LabelComponent();
        }

        public bool HasImage { get; set; }

        public bool IsLiked { get; set; }

        public bool IsLikeDeleted { get; set; }

        public DateTime AddedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public int DislikeCount { get; set; }

        public int LikeCount { get; set; }

        public LabelComponent Text { get; set; }

        public LabelComponent Title { get; set; }

        public long Id { get; set; }
    }
}