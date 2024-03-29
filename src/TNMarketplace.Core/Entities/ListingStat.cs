﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class ListingStat : Entity
    {
        public int ID { get; set; }
        public int CountView { get; set; }
        public int CountSpam { get; set; }
        public int CountRepeated { get; set; }
        public int ListingID { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
