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

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
