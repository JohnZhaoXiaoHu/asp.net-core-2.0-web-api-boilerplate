using System;

namespace CoreApi.Infrastructure.Features.Common
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string LastAction { get; set; }

        public int Order { get; set; }
    }
}
