namespace SalesApi.Infrastructure.Abstractions.DomainModels
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public bool Deleted { get; set; }
    }
}
