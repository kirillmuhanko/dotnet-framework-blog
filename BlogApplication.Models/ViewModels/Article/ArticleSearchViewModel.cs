using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Attributes.FormGroups;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.Models.ViewModels.Article
{
    public class ArticleSearchViewModel
    {
        public ArticleSearchViewModel()
        {
            Query = new TextBoxFormGroupComponent();
        }

        [Display(Name = "Search article")]
        [RequiredFormGroup]
        public TextBoxFormGroupComponent Query { get; set; }
    }
}