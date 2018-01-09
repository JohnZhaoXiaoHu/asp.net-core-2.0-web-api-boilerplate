using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription
{
    public class MilkmanViewModel: EntityBase
    {
        public int SubAreaId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public string Phone { get; set; }
        public string IdentityCardNo { get; set; }
        public string LegacyId { get; set; }
    }
}
