﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Web.Models
{
    public class SimpleCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public string Description { get; set; }
        public int Parent { get; set; }
        public bool Enabled { get; set; }
        public int Ordering { get; set; }
    }
}
