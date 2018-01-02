namespace SalesApi.ViewModels.Mall
{
    public class MallOrderSetPriceViewModel
    {
        public int Id { get; set; }
        public int ProductForMallId { get; set; }
        public int MallCustomerId { get; set; }
        public string Date { get; set; }
        public decimal Price { get; set; }
    }
}
