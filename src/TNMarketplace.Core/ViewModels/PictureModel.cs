﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Core.ViewModels
{
    public class PictureModel
    {
        public int ID { get; set; }

        public int ListingID { get; set; }

        public string Url { get; set; }

        public int Ordering { get; set; }
    }
}
