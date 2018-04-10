using Sales.Core.Bases;

namespace Sales.Infrastructure.UsefulModels.Pagination
{
    public class PaginationBase
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = nameof(EntityBase.Id);
        public int Count { get; set; }

        public int MaxPageSize { get; set; } = 100;
        public int PageCount => Count / PageSize + (Count % PageSize > 0 ? 1 : 0);
    }
}