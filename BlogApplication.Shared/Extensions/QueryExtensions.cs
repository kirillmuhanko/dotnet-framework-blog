using System.Linq;
using BlogApplication.Models.DomainModels.Common;

namespace BlogApplication.Shared.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            var query = queryable.Skip((page - 1) * pageSize).Take(pageSize);
            return query;
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PaginationDomainModel pagination)
        {
            var query = queryable.Page(pagination.PageNumber, pagination.PageSize);
            return query;
        }
    }
}