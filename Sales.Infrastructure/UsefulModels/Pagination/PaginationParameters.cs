using System;
using System.Linq.Expressions;
using Sales.Core.Bases;

namespace Sales.Infrastructure.UsefulModels.Pagination
{
    public class PaginationParameters<T> : PaginationBase where T : EntityBase
    {
        private const int MaxPageSize = 1000;

        public PaginationParameters(PaginationBase paginationBase, Expression<Func<T, object>> orderBy = null)
        {
            PageIndex = paginationBase.PageIndex;
            PageSize = paginationBase.PageSize < MaxPageSize ? paginationBase.PageSize : MaxPageSize;
            OrderBy = orderBy ?? (x => x.Id);
        }

        public PaginationParameters(int pageIndex, int pageSize, Expression<Func<T, object>> orderBy = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize < MaxPageSize ? pageSize : MaxPageSize;
            OrderBy = orderBy ?? (x => x.Id);
        }

        public Expression<Func<T, object>> OrderBy { get; }
    }
}
