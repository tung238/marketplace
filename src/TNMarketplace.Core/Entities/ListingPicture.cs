using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class ListingPicture : Entity
    {
        public int ID { get; set; }
        public int ListingID { get; set; }
        public string Url { get; set; }
        public int Ordering { get; set; }
        public virtual Listing Listing { get; set; }
        //public virtual Picture Picture { get; set; }
    }
}
