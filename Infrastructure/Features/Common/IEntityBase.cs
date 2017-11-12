using System;
using Infrastructure.Features.Order;

namespace Infrastructure.Features.Common
{
    public interface IEntityBase : IOrder, IDeleted
    {
        int Id { get; set; }
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get; set; }
        string CreateUser { get; set; }
        string UpdateUser { get; set; }
        string LastAction { get; set; }
    }
}
