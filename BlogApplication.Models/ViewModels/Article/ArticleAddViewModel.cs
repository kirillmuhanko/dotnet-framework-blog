using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleAddViewModel
    {
        public ArticleAddViewModel()
        {
            Image = new ImageFormGroupComponent();
            Text = new TextAreaFormGroupComponent();
            Title = new TextBoxFormGroupComponent();
        }

        [Display(Name = "Choose image")] public ImageFormGroupComponent Image { get; set; }

        public long Id { get; set; }

        public TextAreaFormGroupComponent Text { get; set; }

        [RequiredFormGroup] public TextBoxFormGroupComponent Title { get; set; }
    }
}