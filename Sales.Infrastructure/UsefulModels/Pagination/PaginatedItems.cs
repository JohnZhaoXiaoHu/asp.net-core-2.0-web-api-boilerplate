using System.Collections.Generic;

namespace Sales.Infrastructure.UsefulModels.Pagination
{
    public class PaginatedItems<TEntity> : PaginationMetadata where TEntity : class
    {
        public IEnumerable<TEntity> Data { get; }

        public PaginatedItems(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
            : base(pageIndex, pageSize, count)
        {
            Data = data;
        }

        public PaginatedItems(PaginationBase paginationBase, int count, IEnumerable<TEntity> data)
            : base(paginationBase, count)
        {
            Data = data;
        }

        public PaginationMetadata GetMetadata()
        {
            return this;
        }
    }
}
