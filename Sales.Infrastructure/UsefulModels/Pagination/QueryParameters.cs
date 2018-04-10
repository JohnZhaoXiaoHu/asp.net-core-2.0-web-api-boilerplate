namespace Sales.Infrastructure.UsefulModels.Pagination
{
    public class QueryParameters : PaginationBase
    {
        public string SearchTerm { get; set; }
        public string Fields { get; set; }
    }
}
