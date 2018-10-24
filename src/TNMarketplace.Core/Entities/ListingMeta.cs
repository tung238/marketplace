using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class ListingMeta : Entity
    {
        public int ID { get; set; }
        public int ListingID { get; set; }
        public int FieldID { get; set; }
        public string Value { get; set; }
        public virtual Listing Listing { get; set; }
        public virtual MetaField MetaField { get; set; }
    }
}
