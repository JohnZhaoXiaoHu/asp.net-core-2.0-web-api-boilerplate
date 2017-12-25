namespace SalesApi.ViewModels.Collective
{
    public class CollectiveOrderSetPriceViewModel
    {
        public int Id { get; set; }
        public int ProductForCollectiveId { get; set; }
        public int CollectiveCustomerId { get; set; }
        public string Date { get; set; }
        public decimal Price { get; set; }
    }
}
