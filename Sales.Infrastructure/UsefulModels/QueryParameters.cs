namespace Sales.Infrastructure.UsefulModels
{
    public class QueryParameters
    {
        private int _pageSize;
        private const int MaxPageSize = 1000;
        public int PageIndex { get; set; } = 0;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < MaxPageSize ? value : MaxPageSize;
        }

        public string SearchTerm { get; set; }
        public string OrderBy { get; set; } = "Id";
        public string Fields { get; set; }
    }
}
