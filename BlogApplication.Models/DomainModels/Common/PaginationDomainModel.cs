namespace BlogApplication.Models.DomainModels.Common
{
    public class PaginationDomainModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }
    }
}