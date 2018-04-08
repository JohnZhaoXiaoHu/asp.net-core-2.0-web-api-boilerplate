namespace SalesApi.Shared
{
    public class PaginationMetadata
    {
        public int PageIndex { get; }

        public int PageSize { get; }

        public int Count { get; }

        public int PageCount => Count / PageSize + (Count % PageSize > 0 ? 1 : 0);

        public PaginationMetadata(int pageIndex, int pageSize, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
        }
    }
}