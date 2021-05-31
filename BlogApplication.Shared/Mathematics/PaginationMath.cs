namespace BlogApplication.Shared.Mathematics
{
    public static class PaginationMath
    {
        public static int FirstRecord(int page, int pageSize)
        {
            var number = SkippedRecords(page, pageSize) + 1;
            return number;
        }

        public static int PageFrom(int page, int pageRange, int totalPages)
        {
            totalPages = BasicMath.Clamp(totalPages, 1, 1_000_000);
            page = BasicMath.Clamp(page, 1, totalPages);
            pageRange = BasicMath.Clamp(pageRange, 1, 101);

            var pageFrom = page - pageRange / 2;

            if (pageRange % 2 == 0) pageFrom++;
            if (page + pageRange / 2 >= totalPages) pageFrom = totalPages - pageRange + 1;
            if (pageFrom <= 0) pageFrom = 1;

            return pageFrom;
        }

        public static int PageTo(int page, int pageRange, int totalPages)
        {
            totalPages = BasicMath.Clamp(totalPages, 1, 1_000_000);
            page = BasicMath.Clamp(page, 1, totalPages);
            pageRange = BasicMath.Clamp(pageRange, 1, 101);

            var pageTo = page + pageRange / 2;

            if (page - pageRange / 2 <= 0) pageTo = pageRange;
            if (pageTo >= totalPages) pageTo = totalPages;

            return pageTo;
        }

        public static int SkippedRecords(int page, int pageSize)
        {
            page = BasicMath.Clamp(page, 1, 1_000_000);
            pageSize = BasicMath.Clamp(pageSize, 1, 1_000);

            var count = (page - 1) * pageSize;
            return count;
        }

        public static int TotalPages(int pageSize, int totalRecords)
        {
            pageSize = BasicMath.Clamp(pageSize, 1, 1_000);
            totalRecords = BasicMath.Clamp(totalRecords, 1, 1_000_000);

            var count = (totalRecords + pageSize - 1) / pageSize;
            return count;
        }
    }
}