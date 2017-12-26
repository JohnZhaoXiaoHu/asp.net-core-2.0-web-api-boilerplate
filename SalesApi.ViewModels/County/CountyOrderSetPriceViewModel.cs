namespace SalesApi.ViewModels.County
{
    public class CountyOrderSetPriceViewModel
    {
        public int Id { get; set; }
        public int ProductForCountyId { get; set; }
        public int CountyAgentId { get; set; }
        public string Date { get; set; }
        public decimal Price { get; set; }
    }
}
