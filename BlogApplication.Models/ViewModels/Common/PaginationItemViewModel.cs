namespace BlogApplication.Models.ViewModels.Common
{
    public class PaginationItemViewModel
    {
        public bool IsActive { get; set; }

        public bool IsDisabled { get; set; }

        public int PageNumber { get; set; }

        public string PageText { get; set; }
    }
}