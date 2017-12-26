using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.County;
using SalesApi.Models.Settings;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.County
{
    public class ProductForCounty : EntityBase
    {
        public ProductForCounty()
        {
            SalesType = SalesType.郊县;
            CountyAgentPrices = new List<CountyAgentPrice>();
            CountyPromotionSeries = new List<CountyPromotionSeries>();
            CountyPromotionSeriesBonuses = new List<CountyPromotionSeriesBonus>();
            CountyPromotionEvents = new List<CountyPromotionEvent>();
            CountyPromotionEventBonuses = new List<CountyPromotionEventBonus>();
            CountyProductSnapshots = new List<CountyProductSnapshot>();
        }

        public int ProductId { get; set; }
        public SalesType SalesType { get; set; }
        public int EquivalentBox { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }

        public Product Product { get; set; }
        public ICollection<CountyAgentPrice> CountyAgentPrices { get; set; }
        public ICollection<CountyPromotionSeries> CountyPromotionSeries { get; set; }
        public ICollection<CountyPromotionSeriesBonus> CountyPromotionSeriesBonuses { get; set; }
        public ICollection<CountyPromotionEvent> CountyPromotionEvents { get; set; }
        public ICollection<CountyPromotionEventBonus> CountyPromotionEventBonuses { get; set; }
        public ICollection<CountyProductSnapshot> CountyProductSnapshots { get; set; }
    }

    public class ProductForCountyConfiguration : EntityBaseConfiguration<ProductForCounty>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ProductForCounty> b)
        {
            b.HasOne(x => x.Product).WithOne(x => x.ProductForCounty).HasForeignKey<ProductForCounty>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
