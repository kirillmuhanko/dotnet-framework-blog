using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.ViewModels.Common;
using BlogApplication.Shared.Mathematics;

namespace BlogApplication.Shared.Extensions
{
    public static class PaginationExtensions
    {
        public static PaginationDomainModel Clamp(this PaginationDomainModel model)
        {
            model.TotalRecords = BasicMath.ClampMin(model.TotalRecords, 0);
            var totalPages = PaginationMath.TotalPages(model.PageSize, model.TotalRecords);
            model.PageNumber = BasicMath.Clamp(model.PageNumber, 1, totalPages);
            model.PageSize = BasicMath.ClampMin(model.PageSize, 1);
            return model;
        }

        public static PaginationViewModel Calculate(this PaginationViewModel model)
        {
            model.PageTo = PaginationMath.PageTo(model.PageNumber, model.PageRange, model.TotalPages);
            model.SkippedRecords = PaginationMath.SkippedRecords(model.PageNumber, model.PageSize);
            model.TotalPages = PaginationMath.TotalPages(model.PageSize, model.TotalRecords);
            model.FirstRecord = PaginationMath.FirstRecord(model.PageNumber, model.PageSize);
            model.PageFrom = PaginationMath.PageFrom(model.PageNumber, model.PageRange, model.TotalPages);
            model.PageNumber = BasicMath.Clamp(model.PageNumber, 1, model.TotalPages);
            return model;
        }
    }
}