using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class MetaCategory : Entity
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int FieldID { get; set; }
        public virtual Category Category { get; set; }
        public virtual MetaField MetaField { get; set; }
    }
}
