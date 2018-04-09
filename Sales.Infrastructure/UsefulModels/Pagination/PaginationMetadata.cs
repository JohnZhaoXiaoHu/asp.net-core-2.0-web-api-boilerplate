namespace Sales.Infrastructure.UsefulModels.Pagination
{
    public class PaginationMetadata: PaginationBase
    {
        public int Count { get; }

        public int PageCount => Count / PageSize + (Count % PageSize > 0 ? 1 : 0);

        public PaginationMetadata(int pageIndex, int pageSize, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
        }

        public PaginationMetadata(PaginationBase paginationBase, int count)
        {
            PageIndex = paginationBase.PageIndex;
            PageSize = paginationBase.PageSize;
            Count = count;
        }
    }
}