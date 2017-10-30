using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Infrastructure.Features.Common
{
    public static class EntityBaseExtensions
    {
        public static void SetCreation(this IEntityBase entity, string userName)
        {
            entity.CreateTime = entity.UpdateTime = DateTime.Now;
            entity.CreateUser = entity.UpdateUser = userName;
            entity.LastAction = "Create";
        }

        public static void SetModification(this IEntityBase entity, string userName)
        {
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUser = userName;
            entity.LastAction = "Modify";
        }
    }
}
