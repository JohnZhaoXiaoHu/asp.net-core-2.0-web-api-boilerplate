using System;

namespace Infrastructure.Features.Common
{
    public static class EntityBaseExtensions
    {
        public static void SetCreation(this IEntityBase entity, string userName, string action = "Create")
        {
            entity.CreateTime = entity.UpdateTime = DateTime.Now;
            entity.CreateUser = entity.UpdateUser = userName;
            entity.LastAction = action;
        }

        public static void SetModification(this IEntityBase entity, string userName, string action = "Modify")
        {
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUser = userName;
            entity.LastAction = action;
        }
    }
}
