using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Retail
{
    public class RetailerViewModel: EntityBase
    {
        public int SubAreaId { get; set; }
        public SalesType SalesType { get; set; }
        public string LegacyCustomerId { get; set; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
