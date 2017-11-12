using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreApi.Models.Angular
{
    public class Model: EntityBase
    {
        public string Name { get; set; }

        public Make Make { get; set; }
        public int MakeId { get; set; }
    }

    public class ModelConfiguration : EntityBaseConfiguration<Model>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Model> b)
        {
            b.ToTable("Models");
            b.Property(x => x.Name).IsRequired().HasMaxLength(255);
            b.HasOne(x => x.Make).WithMany(x => x.Models).HasForeignKey(x => x.MakeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
