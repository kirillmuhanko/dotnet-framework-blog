namespace BlogApplication.Models.ViewModels.Common
{
    public class PaginationViewModel
    {
        public int FirstRecord { get; set; }

        public int PageFrom { get; set; }

        public int PageNumber { get; set; }

        public int PageRange { get; set; } = 5;

        public int PageSize { get; set; }

        public int PageTo { get; set; }

        public int SkippedRecords { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public PaginationItemViewModel GetNextPage(string text)
        {
            return new PaginationItemViewModel
            {
                IsDisabled = PageNumber >= PageTo,
                PageNumber = PageTo + 1,
                PageText = text
            };
        }

        public PaginationItemViewModel GetPage(int pageNumber)
        {
            return new PaginationItemViewModel
            {
                IsActive = pageNumber == PageNumber,
                PageNumber = pageNumber,
                PageText = pageNumber.ToString()
            };
        }

        public PaginationItemViewModel GetPreviousPage(string text)
        {
            return new PaginationItemViewModel
            {
                IsDisabled = PageNumber <= PageTo,
                PageNumber = PageFrom - 1,
                PageText = text
            };
        }
    }
}