using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Web.Models
{
    public class SimpleListingType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ButtonLabel { get; set; }
        public bool PriceEnabled { get; set; }
        public string PriceUnitLabel { get; set; }
        public int OrderTypeID { get; set; }
        public string OrderTypeLabel { get; set; }
        public bool PaymentEnabled { get; set; }
        public bool ShippingEnabled { get; set; }
    }
}
