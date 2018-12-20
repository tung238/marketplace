using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleListingStat
    {
        public int ID { get; set; }
        public int CountView { get; set; }
        public int CountSpam { get; set; }
        public int CountRepeated { get; set; }
        public int ListingID { get; set; }
    }
}
