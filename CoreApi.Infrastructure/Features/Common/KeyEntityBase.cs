using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Infrastructure.Features.Common
{
    public abstract class KeyEntityBase: IKeyEntityBase
    {
        public int Id { get; set; }
    }
}
