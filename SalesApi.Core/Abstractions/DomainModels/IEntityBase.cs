namespace SalesApi.Core.Abstractions.DomainModels
{
    public interface IEntityBase : IOrder, IDeleted
    {
        int Id { get; set; }
    }
}
