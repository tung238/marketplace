using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TNMarketplace.Core.Infrastructure;

namespace TNMarketplace.Core
{
    public abstract class Entity: IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
