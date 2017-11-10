using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CoreApi.Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreApi.Models.Angular
{
    public class Make: EntityBase
    {
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }

        public Make()
        {
            Models = new Collection<Model>();
        }
    }

    public class MakeConfiguration : EntityBaseConfiguration<Make>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Make> b)
        {
            b.ToTable("Makes");
            b.Property(x => x.Name).IsRequired().HasMaxLength(255);
        }
    }
}
