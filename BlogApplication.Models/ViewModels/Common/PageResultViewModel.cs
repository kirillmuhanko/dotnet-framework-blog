namespace BlogApplication.Models.ViewModels.Common
{
    public class PageResultViewModel
    {
        public PageResultViewModel(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}