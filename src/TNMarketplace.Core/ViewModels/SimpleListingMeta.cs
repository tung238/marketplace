using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleListingMeta
    {
        public int ID { get; set; }
        public int ListingID { get; set; }
        public int FieldID { get; set; }
        public string Value { get; set; }
    }
}
