namespace SalesApi.Infrastructure.Abstractions.DomainModels
{
    public interface IEntityBase : IOrder, IDeleted
    {
        int Id { get; set; }
    }
}
